using System;
using System.Threading;
using System.Threading.Tasks;
using Projet_seq_8.Services;

class Program
{
    private static MesService _mesService = new MesService();
    private static OpcService _opcService = new OpcService();

    static async Task Main(string[] args)
    {
        Console.WriteLine("====================================================");
        Console.WriteLine("   DEMARRAGE MES - LECTURE REELLE OPC UA            ");
        Console.WriteLine("====================================================");

        try
        {
            // Connexion au serveur Prosys via le service
            await _opcService.ConnectAsync();
            Console.WriteLine("Surveillance des postes activée...");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERREUR INITIALISATION : {ex.Message}");
            Console.ResetColor();
            return;
        }

        while (true)
        {
            try
            {
                // --- TRAITEMENT POSTE 1 ---
                // Compteur: ns=3;i=1011 | Statut: ns=3;i=1014
                int prod1 = _opcService.ReadNodeValue("ns=3;i=1011");
                int stat1 = _opcService.ReadNodeValue("ns=3;i=1014");
                _mesService.CheckMachine(1, prod1, stat1);

                // --- TRAITEMENT POSTE 2 ---
                // Compteur: ns=3;i=1012 | Statut: ns=3;i=1015
                int prod2 = _opcService.ReadNodeValue("ns=3;i=1012");
                int stat2 = _opcService.ReadNodeValue("ns=3;i=1015");
                _mesService.CheckMachine(2, prod2, stat2);

                // --- TRAITEMENT POSTE 3 ---
                // Compteur: ns=3;i=1013 | Statut: ns=3;i=1016
                int prod3 = _opcService.ReadNodeValue("ns=3;i=1013");
                int stat3 = _opcService.ReadNodeValue("ns=3;i=1016");
                _mesService.CheckMachine(3, prod3, stat3);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Erreur de lecture : {ex.Message}");
                Console.ResetColor();
            }

            // Fréquence de lecture (400ms)
            await Task.Delay(400);
        }
    }
}