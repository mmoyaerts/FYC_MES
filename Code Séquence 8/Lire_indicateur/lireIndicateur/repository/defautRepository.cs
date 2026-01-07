using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace lireIndicateur.repository
{
    internal class defautRepository
    {
        public List<(int, int)> getNbrArret(DateTime dateDebut, DateTime dateFin)
        {
            var result = new List<(int, int)>();

            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
                SELECT
                    id_post,
                    COUNT(*) AS nb_defauts
                FROM public.defaut_poste
                WHERE defaut = true AND date_heure >= @dateDebut
                  AND date_heure < @dateFin
                GROUP BY id_post
                ORDER BY id_post;
            ", conn);

            cmd.Parameters.AddWithValue("dateDebut", dateDebut);
            cmd.Parameters.AddWithValue("dateFin", dateFin);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add((
                    reader.GetInt32(0),       // poste
                    reader.GetInt32(1)       // nbr arret
                ));
            }

            return result;
        }

        public List<(int, TimeSpan)> GetTempsArretParPoste(DateTime dateDebut, DateTime dateFin)
        {
            var result = new List<(int, TimeSpan)>();

            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
                WITH intervals AS (
                    SELECT
                        id_post,
                        date_heure AS start_time,
                        LEAD(date_heure) OVER (
                            PARTITION BY id_post
                            ORDER BY date_heure
                        ) AS end_time
                    FROM defaut_poste
                    WHERE defaut = true AND date_heure >= @dateDebut
                    AND date_heure < @dateFin
                )
                SELECT
                    id_post,
                    SUM(end_time - start_time) AS temps_arret
                FROM intervals
                WHERE end_time IS NOT NULL
                GROUP BY id_post
                ORDER BY id_post;
            ", conn);

            cmd.Parameters.AddWithValue("dateDebut", dateDebut);
            cmd.Parameters.AddWithValue("dateFin", dateFin);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add((
                    reader.GetInt32(0),
                    reader.GetTimeSpan(1)
                ));
            }

            return result;
        }

        public TimeSpan GetTempsArretTotal(DateTime dateDebut, DateTime dateFin)
        {
            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
                WITH raw_intervals AS (
                        SELECT
                            date_heure AS start_time,
                            LEAD(date_heure) OVER (PARTITION BY id_post ORDER BY date_heure) AS end_time
                        FROM defaut_poste
                        WHERE defaut = true
                          AND date_heure >= @dateDebut
                          AND date_heure < @dateFin
                    ),
                    ordered AS (
                        SELECT
                            start_time,
                            end_time,
                            LAG(end_time) OVER (ORDER BY start_time) AS prev_end
                        FROM raw_intervals
                        WHERE end_time IS NOT NULL
                    ),
                    groups AS (
                        SELECT
                            start_time,
                            end_time,
                            SUM(CASE WHEN start_time > prev_end THEN 1 ELSE 0 END) OVER (ORDER BY start_time) AS grp
                        FROM ordered
                    ),
                    intervals_grouped AS (
                        SELECT grp, MIN(start_time) AS start_time, MAX(end_time) AS end_time
                        FROM groups
                        GROUP BY grp
                    )
                    SELECT SUM(end_time - start_time) AS temps_arret_total
                    FROM intervals_grouped;
            ", conn);

            cmd.Parameters.AddWithValue("dateDebut", dateDebut);
            cmd.Parameters.AddWithValue("dateFin", dateFin);

            var result = cmd.ExecuteScalar();
            return result == DBNull.Value ? TimeSpan.Zero : (TimeSpan)result;
        }
    }
}
