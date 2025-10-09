using System;
using System.Collections.Generic;
using System.Windows.Forms;
using gererOF.Models;
using gererOF.Service;

namespace gererOF
{
    public partial class Form1 : Form
    {
        private readonly OFService _ofService;

        public Form1()
        {
            InitializeComponent();

            // Initialisation du service
            _ofService = new OFService();

            // �v�nement de chargement du formulaire
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // R�cup�re tous les OF via le service
                List<OF> ofs = _ofService.GetAllOF();

                // Lie la liste d�OF au DataGridView
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
            // V�rifie qu'une ligne est s�lectionn�e
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez s�lectionner un OF dans la liste.", "Aucune s�lection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // R�cup�re l'objet OF de la ligne s�lectionn�e
            var selectedRow = dataGridView1.SelectedRows[0];
            OF of = selectedRow.DataBoundItem as OF;

            if (of == null)
            {
                MessageBox.Show("Impossible de r�cup�rer l'OF s�lectionn�.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Demande � l'utilisateur la quantit�
            string input = Microsoft.VisualBasic.Interaction.InputBox("Entrez la quantit� � lancer :", "Quantit�", "1");

            if (!int.TryParse(input, out int quantite) || quantite <= 0)
            {
                MessageBox.Show("Veuillez entrer un nombre valide sup�rieur � z�ro.", "Quantit� invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Appelle le service pour lancer l'OF en production
                var ofService = new OFService();
                ofService.LancerOFEnProduction(of, quantite);

                MessageBox.Show($"OF {of.Numero} lanc� en production avec quantit� {quantite}.", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            // R�cup�rer l'OF en cours
            var ofEnCours = _ofService.GetOFActuel();

            // Cr�e un label unique pour afficher le texte
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
                // Utilise directement la m�thode ToString() de ton mod�le
                lblInfo.Text = ofEnCours.ToString();
            }

            panel1.Controls.Add(lblInfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // V�rifie qu'une ligne est s�lectionn�e
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez s�lectionner un OF dans la liste.", "Aucune s�lection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // R�cup�re l'objet OF de la ligne s�lectionn�e
            var selectedRow = dataGridView1.SelectedRows[0];
            OF of = selectedRow.DataBoundItem as OF;

            if (of == null)
            {
                MessageBox.Show("Impossible de r�cup�rer l'OF s�lectionn�.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            detailOF detailOF = new detailOF(of);
            detailOF.Show();
        }
    }
}
