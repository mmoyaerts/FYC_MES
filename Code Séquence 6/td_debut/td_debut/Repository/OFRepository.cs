using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using td_debut.Models;
using td_debut.Service; 

namespace td_debut.Repository
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
        // * Exercice 2 : Implémentation de la Transaction complète    *
        // ************************************************************

        /// Met à jour la quantité de l'OF en cours et sa définition, en utilisant une transaction 
        /// pour assurer le versioning et la cohérence.

        public void UpdateOFEnProductionQuantite(OfEnCours ofToUpdate, int newQuantite,
                                                 int newProgChargement, int newProgDechargement)
        {
            // TODO Exercice 2 : Implémenter la transaction complète BDD.

            // 1. Encodage : Appeler decodeMotService.EncoderProgrammesApiChargement pour obtenir la nouvelle valeur entière
            // du champ apichargementdechargement (méthode EncoderProgrammesApiChargement doit être complétée par l'étudiant) ! 
            int newApiChargementMot = decodeMotService.EncoderProgrammesApiChargement(newProgChargement, newProgDechargement);

            // 2. Gestion de la connexion et de la transaction :
            //    - Récupérer une nouvelle connexion ouverte.
            //    - Débuter une nouvelle transaction.
            using var conn = DatabaseConnector.Instance.GetConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 3. Versioning (Historique) : Insérer la quantité précédente dans ofversionhistory.
                // TODO Exercice 2 : Insérer une ligne dans ofversionhistory.

                // 4. Mise à Jour Quantité : Mettre à jour la colonne quantite dans ofenproduction.
                // TODO Exercice 2 : Mettre à jour ofenproduction.

                // 5. Mise à Jour Définition OF : Mettre à jour la colonne apichargementdechargement dans la table of avec la valeur encodée (newApiChargementMot).
                // TODO Exercice 2 : Mettre à jour la table of.

                // 6. Commit : Valider les opérations.
                // TODO Exercice 2 : Commiter la transaction.
            }
            catch (Exception ex)
            {
                // 7. Rollback : Annuler les opérations en cas d'erreur.
                // TODO Exercice 2 : Effectuer un Rollback et relancer l'exception.
                throw;
            }
        }
    }
}