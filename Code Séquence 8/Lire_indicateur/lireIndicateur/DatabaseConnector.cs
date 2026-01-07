using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lireIndicateur
{
    public sealed class DatabaseConnector
    {
        private static readonly Lazy<DatabaseConnector> _instance =
            new Lazy<DatabaseConnector>(() => new DatabaseConnector());

        private readonly string _connectionString;

        private DatabaseConnector()
        {
            _connectionString = "Host=localhost;Port=5432;Username=user;Password=pass1234;Database=user";
        }

        public static DatabaseConnector Instance => _instance.Value;

        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

    }
}
