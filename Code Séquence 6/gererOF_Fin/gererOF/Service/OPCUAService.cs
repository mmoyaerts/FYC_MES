using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll_OPC;

namespace gererOF.Service
{
    internal class OPCUAService
    {
        public async Task RunApplication(string endpointUrl, OpcUaClientManager opcUaClient)
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
        public void ecrire(string nodeId, OpcUaClientManager opcUaClient, object value)
        {
            opcUaClient.WriteNodeValue(nodeId, value);
        }
    }
}
