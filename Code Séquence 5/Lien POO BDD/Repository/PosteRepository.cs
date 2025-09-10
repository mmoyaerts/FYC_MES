using Lien_POO_BDD;
using Lien_POO_BDD.Models;
using Npgsql;

public class PosteRepository
{
    public void InsertPoste(Poste poste)
    {
        using var conn = DatabaseConnector.Instance.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand(
            "INSERT INTO Poste (nom, adresse_ip, ligne_id) VALUES (@nom, @ip, @ligneId)", conn);
        cmd.Parameters.AddWithValue("nom", poste.name);
        cmd.Parameters.AddWithValue("ip", poste.adress_ip);
        cmd.Parameters.AddWithValue("ligneId", poste.LigneId);

 
    }
}