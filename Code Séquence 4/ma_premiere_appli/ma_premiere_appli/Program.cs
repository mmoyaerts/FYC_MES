using Dll_OPC;

class Program
{
    static void Main(string[] args)
    {
        string endpointUrl = "opc.tcp://ADMINIS-AIHP154.mshome.net:53530/OPCUA/SimulationServer";
        OpcUaClientManager opcUaClient = new OpcUaClientManager(endpointUrl);
        // Utilise Task.Run pour exécuter du code asynchrone
        Task.Run(async () => await RunApplication(endpointUrl, opcUaClient)).Wait();

        try
        {
            string node = "ns=3;i=1010";
            lire(node, opcUaClient);

            Int16 value = 23;
            ecrire(node, opcUaClient, value);

            lire(node, opcUaClient);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
    }

    private static async Task RunApplication(string endpointUrl, OpcUaClientManager opcUaClient)
    {
        try
        {
            Console.WriteLine($"Tentative de connexion au serveur OPC UA sur: {endpointUrl}");

            // Connexion au serveur OPC UA
            await opcUaClient.ConnectAsync();

            if (opcUaClient.IsConnected)
            {
                Console.WriteLine("Connexion au serveur OPC UA réussie.");
            }
            else
            {
                Console.WriteLine("non.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void lire(string nodeId, OpcUaClientManager opcUaClient)
    {
        string value = opcUaClient.ReadValue(nodeId);
        Console.WriteLine($"Valeur lue de {nodeId}: {value}");
    }

    private static void ecrire(string nodeId, OpcUaClientManager opcUaClient, object value)
    {
        opcUaClient.WriteNodeValue(nodeId, value);
    }
}