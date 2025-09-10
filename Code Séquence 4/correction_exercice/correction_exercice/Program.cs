using Dll_OPC;
using Newtonsoft.Json.Linq;
using Opc.Ua;

class Program
{
    private static OpcUaClientManager _opcUaClient;

    //je defini mon objectif de bouteille
    //egal au max definit dans mon serveur opc ua
    public const int objectifDeBouteille = 400;
    //initialisation des variable

    public static int nombreDeBouteilleDefectueuse = 0;
    public static int nombreDeBouteilleFabrique = 0;
    static void Main(string[] args)
    {
        string endpointUrl = "opc.tcp://ADMINIS-AIHP154.mshome.net:53530/OPCUA/SimulationServer";
        _opcUaClient = new OpcUaClientManager(endpointUrl);
        // Je reutilise la même fonction que dans le code de presentation
        // Pour me connecter à mon serveur
        //je renvoie cependant un booleen pour continuer si je suis connecte
        bool estConnecte = Task.Run(async () => await RunApplication(endpointUrl, _opcUaClient)).Result;
        if (estConnecte){
            //j'affiche un menu pour que l'utilisateur choisse ce qu'il veut
            string[] nodeIds = { "ns=3;i=1010"};
            _opcUaClient.SubscribeToNodes(nodeIds, LireBouteilleDefectueuse);
            AfficherMenu();
        }
        else
        {
            Console.ReadLine();
        }
        
    }

    private static async Task<bool> RunApplication(string endpointUrl, OpcUaClientManager opcUaClient)
    {
        try
        {
            Console.WriteLine($"Tentative de connexion au serveur OPC UA sur: {endpointUrl}");

            // Connexion au serveur OPC UA
            await opcUaClient.ConnectAsync();

            if (opcUaClient.IsConnected)
            {
                Console.WriteLine("Connexion au serveur OPC UA réussie.");
                return true;
            }
            else
            {
                Console.WriteLine("non.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private static void AfficherMenu()
    {
        bool quitter = false;

        while (!quitter)
        {
            Console.WriteLine("\n=== Menu de production ===");
            Console.WriteLine("1 - Nombre de bouteilles créées");
            Console.WriteLine("2 - Nombre de bouteilles défectueuses");
            Console.WriteLine("3 - Production réelle");
            Console.WriteLine("4 - Production restante");
            Console.WriteLine("5 - Quitter");
            Console.Write("Votre choix : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    lireBouteilleCreer();
                    Console.WriteLine($"Nombre de bouteille fabriqué : {nombreDeBouteilleFabrique}");
                    break;

                case "2":
                    Console.WriteLine($"Nombre de bouteilles défectueuses : {nombreDeBouteilleDefectueuse}");
                    break;

                case "3":
                    lireBouteilleCreer();
                    Console.WriteLine($"Production réelle : {nombreDeBouteilleFabrique - nombreDeBouteilleDefectueuse}");
                    break;

                case "4":
                    lireBouteilleCreer();
                    Console.WriteLine($"Production restante : {objectifDeBouteille - nombreDeBouteilleFabrique}");
                    break;

                case "5":
                    quitter = true;
                    Console.WriteLine("Fermeture du programme...");
                    break;

                default:
                    Console.WriteLine("Choix invalide, veuillez réessayer.");
                    break;
            }
        }
    }

    private static void lireBouteilleCreer()
    {
        string node = "ns=3;i=1009";
        string value = _opcUaClient.ReadValue(node);
        //je tente de convertir ma valeur qui est un string en entier
        if (int.TryParse(value, out int result))
        {
            nombreDeBouteilleFabrique = result;
        }
        else
        {
            Console.WriteLine("Erreur : valeur non valide pour un entier.");
        }
    }

    private static void LireBouteilleDefectueuse(string nodeId, DataValue dataValue)
    {
        //je tente de transformé la valeur retoruner en int
        if (dataValue?.Value != null && int.TryParse(dataValue.Value.ToString(), out int valeur))
        {
            //je definis arbitrairement que si si ma valeur es inferieur à 2, c'est un defaut
            if (valeur < 2)
            {
                nombreDeBouteilleDefectueuse++;
                calculerPourcentageErreur();
            }
        }
        else
        {
            Console.WriteLine("Valeur invalide pour les bouteilles défectueuses.");
        }
    }

    private static void calculerPourcentageErreur()
    {
        lireBouteilleCreer();
        float pourcentageDefaut = ((float)nombreDeBouteilleDefectueuse / nombreDeBouteilleFabrique) * 100;

        if (pourcentageDefaut > 5)
        {
            Console.WriteLine("DANGER!!!");
        }
    }
}