using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gererOF.Models;
using gererOF.Service;
using Npgsql;
using static System.Windows.Forms.LinkLabel;

namespace gererOF.Repository
{
    public class OFRepository
    {
        private readonly decodeMotService _decodeMotService;

        public OFRepository()
        {
            _decodeMotService = new decodeMotService();
        }
        public List<OF> getAll()
        {
            var result = new List<OF>();

            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
        SELECT 
            o.numeroof,
            o.apichargementdechargement,
            o.apirobotgonflage,
            o.idgonflage,
            p.labelgonflage
        FROM of o
        LEFT JOIN programmegonflage p 
            ON o.idgonflage = p.idgonflage", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var(numChargement, numDechargement) = decodeMotService.
                    ExtraireProgrammesApiChargement(reader.GetInt32("apichargementdechargement"));
                var (numGonflage, ctrlGonflage, numControleGonglage) = decodeMotService.
                    ExtraireInfosControleGonflage(reader.GetInt32("apirobotgonflage"));
                var of = new OF(reader.GetInt32(reader.GetOrdinal("numeroof")), numChargement, numGonflage, ctrlGonflage, null, numDechargement);

                if (ctrlGonflage)
                {
                    of.NumControleGonflage = new programmeGonflage(reader.GetInt32(reader.GetOrdinal("idgonflage")), 
                        reader.GetString(reader.GetOrdinal("labelgonflage")));
                }

                result.Add(of);
            }

            return result;
        }

        public void insertOFEnProduction(OF of, int quantite)
        {
            using var conn = DatabaseConnector.Instance.GetConnection();

            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. Mettre tous les enregistrements existants à false
                using (var updateCmd = new NpgsqlCommand("UPDATE ofenproduction SET isvalid = false", conn, transaction))
                {
                    updateCmd.ExecuteNonQuery();
                }

                // 2. Insérer le nouvel enregistrement avec isvalid = true
                using (var insertCmd = new NpgsqlCommand(@"
            INSERT INTO ofenproduction (numeroof, quantite, datedelancement, isvalid) 
            VALUES (@num, @qte, NOW(), true)", conn, transaction))
                {
                    insertCmd.Parameters.AddWithValue("num", of.Numero);
                    insertCmd.Parameters.AddWithValue("qte", quantite);
                    insertCmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public OfEnCours? GetOFEnProduction()
        {
            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
                SELECT id, numeroof, quantite, datedelancement, isvalid
                FROM ofenproduction
                WHERE isvalid = true
                LIMIT 1", conn);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var ofEnCours = new OfEnCours
                {
                    Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id")),
                    NumeroOF = reader.GetInt32(reader.GetOrdinal("numeroof")),
                    Quantite = reader.GetInt32(reader.GetOrdinal("quantite")),
                    DateDeLancement = reader.GetDateTime(reader.GetOrdinal("datedelancement")),
                    IsValid = reader.GetBoolean(reader.GetOrdinal("isvalid"))
                };

                return ofEnCours;
            }

            return null; // Aucun OF actif
        }
    }
}
