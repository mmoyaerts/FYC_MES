using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gererOF.Models;
using Npgsql;
using static System.Windows.Forms.LinkLabel;

namespace gererOF.Repository
{
    public class programmeGonflageRepository
    {
        public List<programmeGonflage> getAll()
        {
            var result = new List<programmeGonflage>();

            using var conn = DatabaseConnector.Instance.GetConnection();

            using var cmd = new NpgsqlCommand("SELECT idgonflage, labelgonflage FROM programmegonflage", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var programme = new programmeGonflage(reader.GetInt32(reader.GetOrdinal("idgonflage")),
                    reader.GetString(reader.GetOrdinal("labelgonflage")));

                result.Add(programme);
            }

            return result;
        }
    }
}
