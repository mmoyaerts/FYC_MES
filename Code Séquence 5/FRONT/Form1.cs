using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lien_POO_BDD.Models;

namespace FRONT
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Color> etatsPostes;
        private Dictionary<string, Panel> cerclesPostes;
        private Dictionary<string, int[]> donneesProduction;
        private Panel panelLigne;
        private FlowLayoutPanel panelGraph;
        private int xPositionProchainPoste = 100;
        private PosteRepository Posterepo;

        public Form1()
        {
            Text = "Vue simplifiée de la ligne de production";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            // Initialisation des dictionnaires
            etatsPostes = new Dictionary<string, Color>
            {
                { "Découpe", Color.Green },
                { "Assemblage", Color.Yellow },
                { "Peinture", Color.Red }
            };
            cerclesPostes = new Dictionary<string, Panel>();
            donneesProduction = new Dictionary<string, int[]>
            {
                { "Découpe", new int[] { 0, 20, 40, 60, 80 } },
                { "Assemblage", new int[] { 0, 25, 50, 70, 100 } },
                { "Peinture", new int[] { 0, 15, 35, 55, 75 } }
            };

            // Panneau de ligne
            panelLigne = new Panel
            {
                Dock = DockStyle.Top,
                Height = 350,
                BackColor = Color.White
            };
            Controls.Add(panelLigne);

            // Création des postes existants
            foreach (var poste in etatsPostes)
            {
                CreerPoste(panelLigne, poste.Key, poste.Value, xPositionProchainPoste, 80);
                xPositionProchainPoste += 300;
            }

            // FlowLayoutPanel pour graphiques
            panelGraph = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 300,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };
            Controls.Add(panelGraph);

            // Création des graphiques pour les postes existants
            foreach (var poste in donneesProduction)
            {
                Chart chart = CreerGraphique(poste.Key, poste.Value);
                panelGraph.Controls.Add(chart);
            }

            // Bouton pour ajouter un nouveau poste
            Button btnAjouterPoste = new Button
            {
                Text = "Ajouter un poste",
                Width = 200,
                Height = 40,
                Left = 700,
                Top = 20,
                BackColor = Color.LightBlue
            };
            btnAjouterPoste.Click += BtnAjouterPoste_Click;
            Controls.Add(btnAjouterPoste);
            btnAjouterPoste.BringToFront();
        }

        private void BtnAjouterPoste_Click(object sender, EventArgs e)
        {
            // Nouvelle fenêtre pour saisir le poste
            Form fenetreAjouter = new Form
            {
                Text = "Ajouter un poste",
                Width = 350,
                Height = 250,
                StartPosition = FormStartPosition.CenterParent
            };

            Label lblNom = new Label { Text = "Nom du poste :", Left = 20, Top = 20, Width = 100 };
            TextBox txtNom = new TextBox { Left = 130, Top = 20, Width = 150 };

            Label lblCouleur = new Label { Text = "Couleur initiale :", Left = 20, Top = 60, Width = 100 };
            ComboBox cbCouleur = new ComboBox { Left = 130, Top = 60, Width = 150 };
            cbCouleur.Items.AddRange(new string[] { "Vert", "Jaune", "Rouge", "Bleu", "Violet" });
            cbCouleur.SelectedIndex = 0;

            Label lblData = new Label { Text = "Production (ex: 0,10,20) :", Left = 20, Top = 100, Width = 150 };
            TextBox txtData = new TextBox { Left = 170, Top = 100, Width = 110 };

            Button btnValider = new Button { Text = "Ajouter", Left = 100, Top = 150, Width = 120 };
            btnValider.Click += (s, ev) =>
            {
                string nom = txtNom.Text.Trim();
                if (string.IsNullOrEmpty(nom))
                {
                    MessageBox.Show("Nom obligatoire !");
                    return;
                }

                // Conversion couleur sélectionnée
                Color couleur = cbCouleur.SelectedItem.ToString() switch
                {
                    "Vert" => Color.Green,
                    "Jaune" => Color.Yellow,
                    "Rouge" => Color.Red,
                    "Bleu" => Color.Blue,
                    "Violet" => Color.Purple,
                    _ => Color.Gray
                };

                // Conversion des données de production
                string[] parts = txtData.Text.Split(',');
                int[] valeurs = Array.ConvertAll(parts, p => int.TryParse(p, out int val) ? val : 0);

                // Ajout dans le back-end (UI)
                etatsPostes[nom] = couleur;
                donneesProduction[nom] = valeurs;

                // Création de l'objet Poste pour le repository
                Poste nouveauPoste = new Poste(nom, "192.168.0.1", 1);


                // Ajout via le repository (en base)
                Posterepo.InsertPoste(nouveauPoste);

                // Mise à jour du front-end
                CreerPoste(panelLigne, nom, couleur, xPositionProchainPoste, 80);
                xPositionProchainPoste += 300;

                Chart chart = CreerGraphique(nom, valeurs);
                panelGraph.Controls.Add(chart);

                fenetreAjouter.Close();
            };

            fenetreAjouter.Controls.Add(lblNom);
            fenetreAjouter.Controls.Add(txtNom);
            fenetreAjouter.Controls.Add(lblCouleur);
            fenetreAjouter.Controls.Add(cbCouleur);
            fenetreAjouter.Controls.Add(lblData);
            fenetreAjouter.Controls.Add(txtData);
            fenetreAjouter.Controls.Add(btnValider);

            fenetreAjouter.ShowDialog();
        }

        private void CreerPoste(Panel panel, string nom, Color couleur, int x, int y)
        {
            Panel cercle = new Panel { Width = 50, Height = 50, Left = x, Top = y };
            cercle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(etatsPostes[nom]), 0, 0, 48, 48);
            };
            panel.Controls.Add(cercle);
            cerclesPostes[nom] = cercle;

            Label label = new Label { Text = nom, Left = x - 10, Top = y + 60, Width = 100, TextAlign = ContentAlignment.MiddleCenter };
            panel.Controls.Add(label);

            Button btnChangeEtat = new Button { Text = "Changer état", Width = 120, Height = 30, Left = x - 35, Top = y + 90 };
            btnChangeEtat.Click += (s, e) => ChangerEtatPoste(nom);
            panel.Controls.Add(btnChangeEtat);

            Button btnDefaut = new Button { Text = "Mettre en défaut", Width = 120, Height = 30, Left = x - 35, Top = y + 130 };
            btnDefaut.Click += (s, e) => MettrePosteEnDefaut(nom);
            panel.Controls.Add(btnDefaut);

            if (x > 100)
            {
                Label ligne = new Label { BackColor = Color.Gray, Width = 250, Height = 3, Left = x - 250, Top = y + 25 };
                panel.Controls.Add(ligne);
                ligne.SendToBack();
            }
        }

        private void ChangerEtatPoste(string nom)
        {
            Color actuel = etatsPostes[nom];
            Color suivant;

            if (actuel == Color.Green)
                suivant = Color.Yellow;
            else if (actuel == Color.Yellow)
                suivant = Color.Red;
            else
                suivant = Color.Green;

            etatsPostes[nom] = suivant;
            cerclesPostes[nom].Invalidate();
        }

        private void MettrePosteEnDefaut(string nom)
        {
            etatsPostes[nom] = Color.DarkRed;
            cerclesPostes[nom].Invalidate();
        }

        private Chart CreerGraphique(string poste, int[] valeurs)
        {
            Chart chart = new Chart { Width = 300, Height = 200, BackColor = Color.White };
            ChartArea area = new ChartArea();
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Title = "Temps";
            area.AxisY.Title = "Production";
            chart.ChartAreas.Add(area);

            Series serie = new Series { Name = poste, ChartType = SeriesChartType.Line, Color = Color.FromArgb(52, 152, 219), BorderWidth = 3 };
            for (int i = 0; i < valeurs.Length; i++) serie.Points.AddXY(i + 1, valeurs[i]);

            chart.Series.Add(serie);
            chart.Titles.Add(poste);
            chart.Titles[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return chart;
        }
    }
}
