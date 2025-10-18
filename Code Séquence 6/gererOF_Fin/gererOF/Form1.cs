using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dll_OPC;
using gererOF.Models;
using gererOF.Service;

namespace gererOF
{
    public partial class Form1 : Form
    {
        private readonly OFService _ofService;
        private readonly OPCUAService _opCUAService;
        private OpcUaClientManager opcUaClient;

        public Form1()
        {
            InitializeComponent();

            // Initialisation du service
            _ofService = new OFService();
            _opCUAService = new OPCUAService();

            // Événement de chargement du formulaire
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string endpointUrl = "opc.tcp://ADMINIS-AIHP154.mshome.net:53530/OPCUA/SimulationServer";
                opcUaClient = new OpcUaClientManager(endpointUrl);
                Task.Run(async () =>
                {
                    await _opCUAService.RunApplication(endpointUrl, opcUaClient);
                });
                // Récupère tous les OF via le service
                List<OF> ofs = _ofService.GetAllOF();

                // Lie la liste d’OF au DataGridView
                dataGridView1.DataSource = ofs;

                dataGridView1.ReadOnly = true;

                // (Optionnel) Ajuste automatiquement la largeur des colonnes
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                AfficherOFEnCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des OF : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Vérifie qu'une ligne est sélectionnée
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un OF dans la liste.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Récupère l'objet OF de la ligne sélectionnée
            var selectedRow = dataGridView1.SelectedRows[0];
            OF of = selectedRow.DataBoundItem as OF;

            if (of == null)
            {
                MessageBox.Show("Impossible de récupérer l'OF sélectionné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Demande à l'utilisateur la quantité
            string input = Microsoft.VisualBasic.Interaction.InputBox("Entrez la quantité à lancer :", "Quantité", "1");

            if (!int.TryParse(input, out int quantite) || quantite <= 0)
            {
                MessageBox.Show("Veuillez entrer un nombre valide supérieur à zéro.", "Quantité invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Appelle le service pour lancer l'OF en production
                var ofService = new OFService();
                ofService.LancerOFEnProduction(of, quantite, opcUaClient);

                MessageBox.Show($"OF {of.Numero} lancé en production avec quantité {quantite}.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AfficherOFEnCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du lancement de l'OF : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherOFEnCours()
        {
            // Nettoyer le panel
            panel1.Controls.Clear();

            // Récupérer l'OF en cours
            var ofEnCours = _ofService.GetOFActuel();

            // Crée un label unique pour afficher le texte
            Label lblInfo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Location = new Point(10, 10),
                ForeColor = Color.Black
            };

            if (ofEnCours == null)
            {
                lblInfo.Text = "Aucun OF en cours";
                lblInfo.ForeColor = Color.Gray;
            }
            else
            {
                // Utilise directement la méthode ToString() de ton modèle
                lblInfo.Text = ofEnCours.ToString();
            }

            panel1.Controls.Add(lblInfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Vérifie qu'une ligne est sélectionnée
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un OF dans la liste.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Récupère l'objet OF de la ligne sélectionnée
            var selectedRow = dataGridView1.SelectedRows[0];
            OF of = selectedRow.DataBoundItem as OF;

            if (of == null)
            {
                MessageBox.Show("Impossible de récupérer l'OF sélectionné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            detailOF detailOF = new detailOF(of);
            detailOF.Show();
        }
    }
}
