using Npgsql;
using System;

namespace Projet_seq_8
{
    // Singleton pour gérer la connexion à la base de données
    public sealed class DatabaseConnector
    {
        private static readonly Lazy<DatabaseConnector> _instance =
            new Lazy<DatabaseConnector>(() => new DatabaseConnector());

        private readonly string _connectionString;

        private DatabaseConnector()
        {
            // Chaîne de connexion configurée selon votre docker-compose (Port 5433)
            _connectionString = "Host=localhost;Port=5433;Username=postgres;Password=pass1234;Database=Projet_seq_8";
        }

        public static DatabaseConnector Instance => _instance.Value;

        // Ouvre la connexion et la retourne
        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);

            try
            {
                conn.Open();
                return conn;
            }
            catch (NpgsqlException ex)
            {
                // Affichage de l'erreur en rouge dans la console
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERREUR BDD Npgsql]");
                Console.WriteLine($"Code SQLState : {ex.SqlState}");
                Console.WriteLine($"Message      : {ex.Message}");
                Console.ResetColor();

                throw; // Relancer pour que le Repository soit au courant
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERREUR GÉNÉRIQUE CONNEXION]");
                Console.WriteLine($"Message : {ex.Message}");
                Console.ResetColor();

                throw;
            }
        }
    }
}