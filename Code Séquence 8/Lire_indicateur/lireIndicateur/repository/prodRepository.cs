using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace lireIndicateur.repository
{
    internal class prodRepository
    {
        public List<(DateTime heure, int totalPieces, int objectif)>
            GetSommePiecesParHeure(DateTime dateDebut, DateTime dateFin)
        {
            var result = new List<(DateTime, int, int)>();

            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
                SELECT
                    date_trunc('hour', date_prod) AS heure,
                    COALESCE(SUM(nb_pieces), 0) AS total_pieces,
                    700 AS objectif
                FROM production_piece
                WHERE date_prod >= @dateDebut
                  AND date_prod < @dateFin
                GROUP BY heure
                ORDER BY heure;
            ", conn);

            cmd.Parameters.AddWithValue("dateDebut", dateDebut);
            cmd.Parameters.AddWithValue("dateFin", dateFin);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add((
                    reader.GetDateTime(0),   // heure
                    reader.GetInt32(1),       // total pièces
                    reader.GetInt32(2)       // objectif
                ));
            }

            return result;
        }
    }
}
