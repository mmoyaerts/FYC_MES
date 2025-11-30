using Lien_POO_BDD;
using Lien_POO_BDD.Models;
using Npgsql;
using System.Collections.Generic;

public class PosteRepository
{
    // 🔹 Récupérer tous les postes d'une ligne
    public List<Poste> GetPostesByLigne(long ligneId)
    {
        var postes = new List<Poste>();

        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "SELECT id, name, adress_ip, ligne_id FROM Poste WHERE ligne_id = @id", conn);
        cmd.Parameters.AddWithValue("id", ligneId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            postes.Add(new Poste(
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt64(3)
            ));
        }

        return postes;
    }

    // 🔹 Récupérer un poste par son id
    public Poste? GetPoste(long posteId)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "SELECT id, name, adress_ip, ligne_id FROM Poste WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", posteId);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Poste(
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt64(3)
            );
        }

        return null;
    }

    // 🔹 Méthode pour changer l'état d'un poste
    public void UpdatePosteState(long posteId, string newState)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "UPDATE Poste SET etat = @etat WHERE id = @id", conn);

        cmd.Parameters.AddWithValue("etat", newState);
        cmd.Parameters.AddWithValue("id", posteId);

        cmd.ExecuteNonQuery();
    }

    // 🔹 Exemple : changer l'adresse IP
    public void UpdatePosteIp(long posteId, string newIp)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "UPDATE Poste SET adress_ip = @ip WHERE id = @id", conn);

        cmd.Parameters.AddWithValue("ip", newIp);
        cmd.Parameters.AddWithValue("id", posteId);

        cmd.ExecuteNonQuery();
    }
    public void InsertPoste(Poste poste)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "INSERT INTO Poste (nom, adresse_ip, ligne_id) VALUES (@nom, @ip, @ligneId)", conn);

        cmd.Parameters.AddWithValue("nom", poste.name);
        cmd.Parameters.AddWithValue("ip", poste.adress_ip);
        cmd.Parameters.AddWithValue("ligneId", poste.LigneId);

        cmd.ExecuteNonQuery();
    }

    // 🔹 Exemple : changer le nom
    public void UpdatePosteName(long posteId, string newName)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "UPDATE Poste SET name = @name WHERE id = @id", conn);

        cmd.Parameters.AddWithValue("name", newName);
        cmd.Parameters.AddWithValue("id", posteId);

        cmd.ExecuteNonQuery();
    }


}
