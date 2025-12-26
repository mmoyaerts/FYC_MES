using System;
using System.Threading.Tasks;
using Dll_OPC; 

namespace Projet_seq_8.Services
{
    public class OpcService
    {
        // On utilise le manager de la DLL comme dans votre ancien projet
        private readonly OpcUaClientManager _opcUaClient;
        private readonly string _endpointUrl = "opc.tcp://PC_de_Liam:53530/OPCUA/SimulationServer";

        public OpcService()
        {
            // Initialisation du client manager avec l'URL cible
            _opcUaClient = new OpcUaClientManager(_endpointUrl);
        }

        /// <summary>
        /// Initialise la connexion au serveur (équivalent à RunApplication)
        /// </summary>
        public async Task ConnectAsync()
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Tentative de connexion OPC UA : {_endpointUrl}");

                // Appel de la méthode de connexion de la DLL
                await _opcUaClient.ConnectAsync();

                if (_opcUaClient.IsConnected)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Connexion réussie au serveur.");
                    Console.ResetColor();
                }
                else
                {
                    throw new Exception("La connexion a échoué sans erreur explicite.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERREUR FATALE OPC] : {ex.Message}");
                Console.ResetColor();
                throw; // On relance pour bloquer le démarrage du Program.cs
            }
        }

        /// <summary>
        /// Lit la valeur d'un NodeId (reprend la logique 'lire')
        /// </summary>
        public int ReadNodeValue(string nodeId)
        {
            try
            {
                if (_opcUaClient == null || !_opcUaClient.IsConnected)
                {
                    Console.WriteLine("[AVERTISSEMENT] Client non connecté, tentative de lecture impossible.");
                    return 0;
                }

                // Utilisation de la méthode 'ReadValue' de votre DLL
                object value = _opcUaClient.ReadValue(nodeId);

                return value != null ? Convert.ToInt32(value) : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERREUR LECTURE] Node {nodeId} : {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Écrit une valeur (reprend la logique 'ecrire')
        /// </summary>
        public void WriteNodeValue(string nodeId, object value)
        {
            if (_opcUaClient != null && _opcUaClient.IsConnected)
            {
                _opcUaClient.WriteNodeValue(nodeId, value);
            }
        }

        public void Disconnect()
        {
            if (_opcUaClient != null && _opcUaClient.IsConnected)
            {
                _opcUaClient.Disconnect();
            }
        }
    }
}