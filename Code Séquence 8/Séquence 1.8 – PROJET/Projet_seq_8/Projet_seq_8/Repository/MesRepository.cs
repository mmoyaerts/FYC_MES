using Npgsql;
using System;
using Projet_seq_8;

namespace Projet_seq_8.Repository
{
    public class MesRepository
    {
        public void InsertProduction(short idPost, int nbPieces)
        {
            try
            {
                using (var conn = DatabaseConnector.Instance.GetConnection())
                {
                    // Utilisation de NOW() pour la date gérée par la BDD
                    string sql = "INSERT INTO production_piece (date_prod, nb_pieces, id_post) VALUES (NOW(), @nb, @id)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("nb", (short)nbPieces);
                        cmd.Parameters.AddWithValue("id", idPost);
                        cmd.ExecuteNonQuery();
                    }
                }

                // --- LOG DE RÉUSSITE ---
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [BDD] INSERTION PRODUCTION : Poste {idPost} | +{nbPieces} pièce(s)");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERREUR BDD] Échec insertion production : {ex.Message}");
                Console.ResetColor();
            }
        }

        public void InsertDefaut(short idPost, bool estEnDefaut)
        {
            try
            {
                using (var conn = DatabaseConnector.Instance.GetConnection())
                {
                    string sql = "INSERT INTO defaut_poste (id_post, defaut, date_heure) VALUES (@id, @defaut, NOW())";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("id", idPost);
                        cmd.Parameters.AddWithValue("defaut", estEnDefaut);
                        cmd.ExecuteNonQuery();
                    }
                }

                // --- LOG DE RÉUSSITE ---
                Console.ForegroundColor = estEnDefaut ? ConsoleColor.Red : ConsoleColor.Blue;
                string etat = estEnDefaut ? "DÉFAUT APPARU" : "DÉFAUT DISPARU (OK)";
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [BDD] ÉTAT POSTE : Poste {idPost} | {etat}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERREUR BDD] Échec insertion défaut : {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}