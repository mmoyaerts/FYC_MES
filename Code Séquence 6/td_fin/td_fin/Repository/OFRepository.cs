using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using td_fin.Models;
using td_fin.Service; 

namespace td_fin.Repository
{
    public class OFRepository
    {
        public OFRepository()
        {
        }

        // ************************************************************
        // * Récupération OF en cours                                 *
        // ************************************************************
        public OfEnCours? GetOFEnProduction()
        {
            using var conn = DatabaseConnector.Instance.GetConnection();
            using var cmd = new NpgsqlCommand("SELECT * FROM ofenproduction WHERE isvalid = TRUE LIMIT 1", conn);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new OfEnCours
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    NumeroOF = reader.GetInt16(reader.GetOrdinal("numeroof")),
                    Quantite = reader.GetInt32(reader.GetOrdinal("quantite")),
                    DateDeLancement = reader.GetDateTime(reader.GetOrdinal("datedelancement")),
                    IsValid = reader.GetBoolean(reader.GetOrdinal("isvalid"))
                };
            }
            return null;
        }

        // ************************************************************
        // * Implémentation de GetAllOFs()                            *
        // ************************************************************
        public List<OF> GetAllOFs()
        {
            var ofs = new List<OF>();

            using var conn = DatabaseConnector.Instance.GetConnection();
            using var cmd = new NpgsqlCommand(@"
                SELECT 
                    o.numeroof, 
                    o.apichargementdechargement, 
                    o.apirobotgonflage, 
                    pg.idgonflage, 
                    pg.labelgonflage
                FROM Of o
                LEFT JOIN programmeGonflage pg ON o.idGonflage = pg.idGonflage", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int apiChargementDechargement = reader.GetInt16(reader.GetOrdinal("apichargementdechargement"));

                // Utilisation des méthodes d'extraction de decodeMotService
                int numChargement = decodeMotService.ExtraireProgrammeChargement(apiChargementDechargement);
                int numDechargement = decodeMotService.ExtraireProgrammeDechargement(apiChargementDechargement);

                // Récupération du programme de gonflage (peut être NULL)
                programmeGonflage? progGonflage = null;
                if (!reader.IsDBNull(reader.GetOrdinal("idgonflage")))
                {
                    progGonflage = new programmeGonflage(
                        reader.GetInt32(reader.GetOrdinal("idgonflage")),
                        reader.GetString(reader.GetOrdinal("labelgonflage")));
                }

                ofs.Add(new OF(
                    numero: reader.GetInt16(reader.GetOrdinal("numeroof")),
                    numRobotChargement: numChargement,
                    numRobotGonflage: reader.GetInt16(reader.GetOrdinal("apirobotgonflage")),
                    controleGonflage: progGonflage != null,
                    numControleGonflage: progGonflage,
                    numRobotDechargement: numDechargement
                ));
            }
            return ofs;
        }



        // ************************************************************
        // * Exercice 2 : Implémentation de la Transaction complète   *
        // ************************************************************

        /// Met à jour la quantité de l'OF en cours et sa définition, en utilisant une transaction 
        /// pour assurer le versioning et la cohérence.

        public void UpdateOFEnProductionQuantite(OfEnCours ofToUpdate, int newQuantite,
                                                 int newProgChargement, int newProgDechargement)
        {
            // 1. Encodage : Calcul de la nouvelle valeur encodée
            int newApiChargementMot = decodeMotService.EncoderProgrammesApiChargement(newProgChargement, newProgDechargement);

            // 2. Récupérer la connexion (déjà ouverte par GetConnection())
            using var conn = DatabaseConnector.Instance.GetConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                // A. Versioning (Historique) : Enregistrement de l'état précédent
                using (var insertHistoryCmd = new NpgsqlCommand(@"
                    INSERT INTO ofversionhistory (numeroof, quantiteprecedente, datehistorique)
                    VALUES (@numOF, @oldQte, NOW())", conn, transaction))
                {
                    insertHistoryCmd.Parameters.AddWithValue("numOF", ofToUpdate.NumeroOF);
                    insertHistoryCmd.Parameters.AddWithValue("oldQte", ofToUpdate.Quantite);
                    insertHistoryCmd.ExecuteNonQuery();
                }

                // B. Mise à Jour Quantité dans 'ofenproduction'
                using (var updateOFProdCmd = new NpgsqlCommand(@"
                    UPDATE ofenproduction SET quantite = @newQte
                    WHERE id = @idToUpdate", conn, transaction))
                {
                    updateOFProdCmd.Parameters.AddWithValue("newQte", newQuantite);
                    updateOFProdCmd.Parameters.AddWithValue("idToUpdate", ofToUpdate.Id);
                    updateOFProdCmd.ExecuteNonQuery();
                }

                // C. Mise à Jour Définition OF (table 'of') : Mise à jour du mot API encodé
                using (var updateOFDefCmd = new NpgsqlCommand(@"
                    UPDATE of SET apichargementdechargement = @newApi
                    WHERE numeroof = @numOF", conn, transaction))
                {
                    updateOFDefCmd.Parameters.AddWithValue("newApi", newApiChargementMot);
                    updateOFDefCmd.Parameters.AddWithValue("numOF", ofToUpdate.NumeroOF);
                    updateOFDefCmd.ExecuteNonQuery();
                }

                // D. Commit de la transaction : Valide toutes les opérations
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // E. Rollback en cas d'erreur : Annule toutes les opérations
                transaction.Rollback();
                // On relance l'exception pour que le Form1 puisse afficher un message d'erreur
                throw new Exception($"Erreur lors de la transaction de mise à jour de l'OF : {ex.Message}", ex);
            }
        }
    }
}