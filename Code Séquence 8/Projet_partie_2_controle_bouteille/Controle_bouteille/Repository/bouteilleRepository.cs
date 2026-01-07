using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Controle_bouteille.Models;
using Npgsql;

namespace Controle_bouteille.Repository
{
    internal class bouteilleRepository
    {
        public void insererBouteille(Bouteille bouteille)
        {
            using var conn = DatabaseConnector.Instance.GetConnection();
            try
            {
                using (var insertCmd = new NpgsqlCommand(@"
                INSERT INTO control_final (code_tracabilite, poid, presence_bouchon, resistance_air, bouteille_conforme, date_controle)
                 VALUES (@code, @poid, @bouchon, @resistance, @conforme, NOW())", conn))
                {
                    insertCmd.Parameters.AddWithValue("code", bouteille.CodeTracabilite);
                    insertCmd.Parameters.AddWithValue("poid", bouteille.Poids);
                    insertCmd.Parameters.AddWithValue("bouchon", bouteille.PresenceBouchon);
                    insertCmd.Parameters.AddWithValue("resistance", bouteille.ResistanceAir);
                    insertCmd.Parameters.AddWithValue("conforme", bouteille.Conforme);
                    insertCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
