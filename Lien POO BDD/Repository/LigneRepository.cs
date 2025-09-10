using Lien_POO_BDD;
using Lien_POO_BDD.Models;
using Npgsql;

public class LigneRepository
{
    public void InsertLigne(Ligne ligne)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        using var cmd = new NpgsqlCommand(
            "INSERT INTO Ligne (nom) VALUES (@nom)", conn);
        cmd.Parameters.AddWithValue("nom", ligne.name);
    }
}