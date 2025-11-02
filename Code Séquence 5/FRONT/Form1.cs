using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FRONT
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Color> etatsPostes;
        private Dictionary<string, int[]> donneesProduction;

        public Form1()
        {
            Text = "Vue simplifiée de la ligne de production";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            // Bouton pour afficher la légende
            Button btnLegende = new Button
            {
                Text = "Afficher la légende",
                Width = 200,
                Height = 40,
                Left = 950,
                Top = 20,
                BackColor = Color.LightGray
            };
            btnLegende.Click += (s, e) =>
            {
                FormLegende fenetreLegende = new FormLegende();
                fenetreLegende.ShowDialog();
            };
            Controls.Add(btnLegende);

            // Données
            etatsPostes = new Dictionary<string, Color>
            {
                { "Découpe", Color.Green },
                { "Assemblage", Color.Yellow },
                { "Peinture", Color.Red }
            };

            donneesProduction = new Dictionary<string, int[]>
            {
                { "Découpe", new int[] { 0, 20, 40, 60, 80 } },
                { "Assemblage", new int[] { 0, 25, 50, 70, 100 } },
                { "Peinture", new int[] { 0, 15, 35, 55, 75 } }
            };

            // Panneau de ligne
            Panel panelLigne = new Panel
            {
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = Color.White
            };
            Controls.Add(panelLigne);

            int x = 100;
            foreach (var poste in etatsPostes)
            {
                CreerPoste(panelLigne, poste.Key, poste.Value, x, 100);
                x += 300;
            }

            // Graphiques
            FlowLayoutPanel panelGraph = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 300,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };
            Controls.Add(panelGraph);

            foreach (var poste in donneesProduction)
            {
                Chart chart = CreerGraphique(poste.Key, poste.Value);
                panelGraph.Controls.Add(chart);
            }
        }

        private void CreerPoste(Panel panel, string nom, Color couleur, int x, int y)
        {
            Panel cercle = new Panel
            {
                Width = 50,
                Height = 50,
                Left = x,
                Top = y
            };

            cercle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(couleur), 0, 0, 48, 48);
            };
            panel.Controls.Add(cercle);

            Label label = new Label
            {
                Text = nom,
                Left = x - 10,
                Top = y + 60,
                Width = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            panel.Controls.Add(label);

            // Lien entre postes
            if (x > 100)
            {
                Label ligne = new Label
                {
                    BackColor = Color.Gray,
                    Width = 250,
                    Height = 3,
                    Left = x - 250,
                    Top = y + 25
                };
                panel.Controls.Add(ligne);
                ligne.SendToBack();
            }
        }

        private Chart CreerGraphique(string poste, int[] valeurs)
        {
            Chart chart = new Chart
            {
                Width = 300,
                Height = 200,
                BackColor = Color.White
            };

            ChartArea area = new ChartArea();
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Title = "Temps";
            area.AxisY.Title = "Production";
            chart.ChartAreas.Add(area);

            Series serie = new Series
            {
                Name = poste,
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(52, 152, 219),
                BorderWidth = 3
            };

            for (int i = 0; i < valeurs.Length; i++)
                serie.Points.AddXY(i + 1, valeurs[i]);

            chart.Series.Add(serie);
            chart.Titles.Add(poste);
            chart.Titles[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return chart;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
