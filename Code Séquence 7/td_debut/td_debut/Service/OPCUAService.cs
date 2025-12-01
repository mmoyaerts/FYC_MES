using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dll_OPC;

namespace td_debut.Service
{
    internal class OPCUAService
    {
        // Méthode existante (fournie)
        public async Task RunApplication(string endpointUrl, OpcUaClientManager opcUaClient)
        {
            try
            {
                // Remplacement des Console.WriteLine par des MessageBox pour l'IHM
                MessageBox.Show($"Tentative de connexion au serveur OPC UA sur: {endpointUrl}");
                await opcUaClient.ConnectAsync();

                if (opcUaClient.IsConnected)
                {
                    MessageBox.Show("Connexion au serveur OPC UA réussie.");
                }
                else
                {
                    MessageBox.Show("Erreur de connexion : le client n'est pas connecté.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur fatale lors de la connexion OPC UA: {ex.Message}");
            }
        }

        // Méthode existante (fournie)
        /// Écrit une valeur dans un NodeId spécifique sur le serveur OPC UA.
        // OPCUAService.cs
        public void ecrire(string nodeId, OpcUaClientManager opcUaClient, object value)
        {
            if (opcUaClient == null || !opcUaClient.IsConnected)
            {
                // En cas d'erreur de connexion, nous n'arrêtons pas le programme, nous logons juste.
                Console.WriteLine("Erreur d'écriture OPC UA: Le client n'est pas connecté.");
                return;
            }
            // Cette ligne appelle la librairie OPC UA pour écrire
            opcUaClient.WriteNodeValue(nodeId, value);
        }

        // ************************************************************
        // * Lecture OPC UA fournie (nécessaire au suivi)             *
        // ************************************************************
        /// Lit la valeur d'un NodeId spécifique sur le serveur OPC UA.
        /// <returns>La valeur lue du nœud (castée en object).</returns>
        public object lire(string nodeId, OpcUaClientManager opcUaClient)
        {
            if (opcUaClient == null || !opcUaClient.IsConnected)
            {
                throw new Exception("Le client OPC UA n'est pas connecté pour la lecture.");
            }
            // Code de lecture OPC UA
            return opcUaClient.ReadValue(nodeId);
        }
    }
}