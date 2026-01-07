using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace lireIndicateur.repository
{
    internal class controlRepository
    {
        public (int total, int nonConformes, int mauvaisPoid, int mauvaiseResi, int mauvaisBouchon)
    getInfoControl(DateTime dateDebut, DateTime dateFin)
        {
            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand(@"
            SELECT
                COUNT(*) AS total_lignes,
                COUNT(*) FILTER (WHERE bouteille_conforme = false) AS non_conformes,
                COUNT(*) FILTER (WHERE poid < 20 OR poid > 50) AS mauvais_poid,
                COUNT(*) FILTER (WHERE resistance_air < 6 OR resistance_air > 16) AS mauvais_resi,
                COUNT(*) FILTER (WHERE presence_bouchon = false) AS mauvais_bouchon
            FROM control_final
            WHERE date_controle >= @dateDebut
              AND date_controle < @dateFin;", conn);

            cmd.Parameters.AddWithValue("dateDebut", dateDebut);
            cmd.Parameters.AddWithValue("dateFin", dateFin);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return (
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4)
                );
            }

            // Valeur par défaut si aucune ligne
            return (0, 0, 0, 0, 0);
        }

    }
}
