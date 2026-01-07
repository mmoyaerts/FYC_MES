using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dll_OPC;
using td_debut.Models;
using td_debut.Service;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.VisualBasic;

namespace td_debut
{
    internal partial class Form1 : Form
    {
        // ************************************************************
        // * DÉCLARATIONS DES SERVICES ET DES DÉPENDANCES             *
        // ************************************************************
        private readonly OFService _ofService;
        private readonly OPCUAService _opCUAService;
        private OpcUaClientManager? opcUaClient;
        private const string EndpointUrl = "opc.tcp://localhost:53530/OPCUA/SimulationServer";


        // ************************************************************
        // * CONSTRUCTEUR                                             *
        // ************************************************************
        public Form1()
        {
            InitializeComponent();

            _ofService = new OFService();
            _opCUAService = new OPCUAService();
            this.Load += Form1_Load;
        }

        // ************************************************************
        // * ÉVÉNEMENT DE CHARGEMENT DE LA FENÊTRE                    *
        // ************************************************************
        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                // 1. Connexion OPC UA
                opcUaClient = new OpcUaClientManager(EndpointUrl);
                Task.Run(async () =>
                {
                    await _opCUAService.RunApplication(EndpointUrl, opcUaClient);
                });

                // 2. Affichage des OF dans la grille
                List<OF> ofs = _ofService.GetAllOF();

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = ofs;
                    dataGridView1.ReadOnly = true;
                }

                // 3. Affiche l'OF en cours
                AfficherOFEnCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement ou de la connexion: {ex.Message}",
                    "Erreur Fatale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ************************************************************
        // * Logique IHM (Affichage et Actions)                       *
        // ************************************************************

        /// Affiche l'OF actuellement en production dans le panneau 'panel1'.
        private void AfficherOFEnCours()
        {
            if (panel1 == null) return;

            panel1.Controls.Clear();
            var ofEnCours = _ofService.GetOFActuel();

            if (ofEnCours != null)
            {
                Label lblInfo = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    Location = new Point(10, 10),
                    ForeColor = Color.Black,
                    Text = ofEnCours.ToString()
                };
                panel1.Controls.Add(lblInfo);
            }
            else
            {
                Label lblInfo = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    Location = new Point(10, 10),
                    ForeColor = Color.Gray,
                    Text = "Aucun OF en cours de production."
                };
                panel1.Controls.Add(lblInfo);
            }
        }

        // ************************************************************
        // * LOGIQUE DE MODIFICATION (Exercice 2 + Extension OPC UA) *
        // ************************************************************
        private void btnModifierOF_Click(object sender, EventArgs e)
        {
            // Simulation des nouvelles valeurs
            int nouvelleQuantite = 600;
            int newProgChargement = 4;
            int newProgDechargement = 5;

            // Vérifie le statut de la connexion OPC UA
            bool isOpcUaConnected = opcUaClient != null && opcUaClient.IsConnected;
            // On passe l'objet client (ou null si non connecté)
            OpcUaClientManager? clientToWrite = isOpcUaConnected ? opcUaClient : null;

            try
            {
                var ofActuel = _ofService.GetOFActuel();
                if (ofActuel != null)
                {
                    // 1. Exécute la transaction BDD et tente l'écriture OPC UA via le Service
                    _ofService.UpdateOFQuantite(
                        ofActuel,
                        nouvelleQuantite,
                        newProgChargement,
                        newProgDechargement,
                        clientToWrite // <<< Passage du client OPC UA
                    );

                    // 2. Mise à jour de l'objet local pour le rafraîchissement
                    ofActuel.Quantite = nouvelleQuantite;

                    // 3. Message de confirmation (incluant le statut OPC UA)
                    string message = $"L'OF {ofActuel.NumeroOF} a été modifié (Quantité: {nouvelleQuantite}).\n" +
                                     "Vérifiez les tables (ofenproduction, of, ofversionhistory).";

                    if (isOpcUaConnected)
                    {
                        message += $"\n\nProgramme Chargement ({newProgChargement}) ENVOYÉ à l'automate (OPC UA Node: ns=3;i=1003).";
                    }
                    else
                    {
                        message += "\n\nATTENTION: La connexion OPC UA était absente, l'automate n'a PAS reçu la nouvelle consigne.";
                    }

                    MessageBox.Show(message, "Modification OF Réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Rafraîchit l'affichage
                    AfficherOFEnCours();
                }
                else
                {
                    MessageBox.Show("Aucun Ordre de Fabrication n'est en cours de production.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur Système", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}