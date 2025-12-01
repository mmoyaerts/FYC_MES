using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td_debut.Models;
using System.Windows.Forms;

namespace td_debut
{
    // Singleton pour gérer la connexion à la base de données
    public sealed class DatabaseConnector
    {
        private static readonly Lazy<DatabaseConnector> _instance =
            new Lazy<DatabaseConnector>(() => new DatabaseConnector());

        private readonly string _connectionString;

        // DatabaseConnector.cs
        private DatabaseConnector()
        {
            // Chaîne de connexion corrigée : 
            // - Host: 127.0.0.1 (local)
            // - Username: postgres (Superutilisateur pour éviter l'erreur 28000)
            // - Database: td_debut (base définie dans docker-compose.yml)
            _connectionString = "Host=localhost;Port=5433;Username=postgres;Password=pass1234;Database=td_debut";
        }

        public static DatabaseConnector Instance => _instance.Value;

        // Ouvre la connexion et la retourne, avec ajout d'un diagnostic d'erreur
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
                // Affiche l'erreur Npgsql spécifique
                MessageBox.Show($"[ERREUR BDD Npgsql]\nCode SQLState: {ex.SqlState}\nMessage: {ex.Message}",
                                "Erreur de Connexion PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Relancer l'exception pour que le code appelant (Repository) la gère
            }
            catch (Exception ex)
            {
                // Affiche toute autre erreur inattendue
                MessageBox.Show($"[ERREUR GÉNÉRIQUE]\nMessage: {ex.Message}",
                                "Erreur Inattendue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}