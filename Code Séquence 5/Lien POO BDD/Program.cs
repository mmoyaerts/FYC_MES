using Lien_POO_BDD;
using Lien_POO_BDD.Models;

class Program
{
    static void Main()
    {
        var ligneRepo = new LigneRepository();
        var posteRepo = new PosteRepository();

        // Création d'une ligne
        var ligneA = new Ligne("Ligne B");
        ligneRepo.InsertLigne(ligneA);

        // Création des postes associés
        var poste1 = new Poste ("Poste1", "192.168.0.10", ligneA.id);
        var poste2 = new Poste ("Poste2", "192.168.0.11", ligneA.id);

        posteRepo.InsertPoste(poste1);
        posteRepo.InsertPoste(poste2);

        Console.WriteLine($"Ligne {ligneA.name} insérée avec ID = {ligneA.id}");
        Console.WriteLine($"Poste {poste1.name} inséré avec ID = {poste1.id}");
        Console.WriteLine($"Poste {poste2.name} inséré avec ID = {poste2.id}");
    }
}