using System;
using System.Threading.Tasks;
using Controle_bouteille.Service;
using Dll_OPC;
using Opc.Ua;

namespace Controle_bouteille
{
    class Program
    {
        // La méthode Main doit être statique et retourne Task pour être le point d'entrée asynchrone.
        static async Task Main(string[] args)
        {
            bouteilleService bouteilleService = new bouteilleService();
            OPCUAService opCUAService = new OPCUAService();
            OpcUaClientManager opcUaClient;

            try
            {
                string endpointUrl = "opc.tcp://ADMINIS-AIHP154.mshome.net:53530/OPCUA/SimulationServer";

                opcUaClient = new OpcUaClientManager(endpointUrl);
                await opCUAService.RunApplication(endpointUrl, opcUaClient);

                opcUaClient.SubscribeToNodes(
                    new[] { "ns=3;i=1021" },
                    (nodeId, value) => bouteilleService.insererBouteille(opcUaClient)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine(); // pour garder la console ouverte
        }
    }
}
