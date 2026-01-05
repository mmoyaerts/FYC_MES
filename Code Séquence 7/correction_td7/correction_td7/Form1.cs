using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BACK.Logs;
using BACK.Models;
using BACK.Services;
using Dll_OPC;


namespace FRONT
{
        public partial class FormMain : Form
        {
            private readonly OpcUaClientManager opc;
            private FlowLayoutPanel panelGraph;
            private readonly List<Ligne> lignes;
            private readonly Dictionary<Poste, Panel> affichagePostes = new();
            private Dictionary<string, List<int>> historiqueValeursPostes;
            private Dictionary<string, Chart> graphiquesPostes;
            private Dictionary<string, string> nodeIdsValeursPostes;

            private System.Windows.Forms.Timer timer;

            public FormMain()
            {
                Text = "MES – Supervision ligne";
                Width = 1200;
                Height = 700;
                opc = new OpcUaClientManager("opc.tcp://PC_DE_MAT:53530/OPCUA/SimulationServer");
                Logger.Log("Connexion OPC UA réussie");
                Task.Run(async () => await ConnecterOpcUaAsync()).Wait();

                lignes = new LigneService().CreerLignes();

                historiqueValeursPostes = new Dictionary<string, List<int>>();
                graphiquesPostes = new Dictionary<string, Chart>();

                nodeIdsValeursPostes = new Dictionary<string, string>
                {
                    { "Découpe", "ns=3;i=1002" },
                    { "Assemblage", "ns=3;i=1005" },
                    { "Peinture", "ns=3;i=1012" },
                    { "Emballage", "ns=3;i=1018" },
                    { "Contrôle", "ns=3;i=1020" },
                    { "Expédition", "ns=3;i=1023" }
                };

                panelGraph = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 280,
                    BackColor = Color.WhiteSmoke,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoScroll = true,
                    Padding = new Padding(10)
                };

                Controls.Add(panelGraph);
                foreach (var poste in nodeIdsValeursPostes.Keys)
                {
                    Chart chart = CreerGraphiquePoste(poste);
                    panelGraph.Controls.Add(chart);
                }
                CreerInterface();
                InitialiserTimer();

                Button btnLegende = new Button
                {
                    Text = "Afficher la légende",
                    Width = 200,
                    Height = 40,
                    Left = 950,
                    Top = 20
                };

                btnLegende.Click += (s, e) =>
                {
                    FormLegende legende = new FormLegende();
                    legende.ShowDialog();
                };

                Controls.Add(btnLegende);

            }
        private void MettreAJourValeurPosteDepuisOPC(string nomPoste)
        {
            if (!nodeIdsValeursPostes.ContainsKey(nomPoste))
                return;

            string valeurStr = opc.ReadValue(nodeIdsValeursPostes[nomPoste]);

            if (!int.TryParse(valeurStr, out int valeur))
                return;

            List<int> historique = historiqueValeursPostes[nomPoste];
            historique.Add(valeur);

            // Limite de l'historique (ex : 20 points)
            if (historique.Count > 20)
                historique.RemoveAt(0);

            // Mise à jour graphique
            Chart chart = graphiquesPostes[nomPoste];
            Series serie = chart.Series[0];
            serie.Points.Clear();

            for (int i = 0; i < historique.Count; i++)
                serie.Points.AddXY(i + 1, historique[i]);
        }


        private Chart CreerGraphiquePoste(string nomPoste)
            {
                Chart chart = new Chart
                {
                    Width = 300,
                    Height = 200,
                    BackColor = Color.White
                };

                ChartArea area = new ChartArea();
                area.AxisX.Title = "Temps";
                area.AxisY.Title = "Valeur OPC";
                area.AxisY.Minimum = 0;
                area.AxisY.Maximum = 100;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.LightGray;

                chart.ChartAreas.Add(area);

                Series serie = new Series
                {
                    Name = nomPoste,
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.DodgerBlue
                };

                chart.Series.Add(serie);
                chart.Titles.Add(nomPoste);

                // Initialisation de l'historique
                historiqueValeursPostes[nomPoste] = new List<int>();

                graphiquesPostes[nomPoste] = chart;

                return chart;
            }


            private async Task ConnecterOpcUaAsync()
            {
                try
                {
                    await opc.ConnectAsync();
                    MessageBox.Show("Connexion OPC UA OK");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            private void CreerInterface()
            {
                int y = 50;

                foreach (var ligne in lignes)
                {
                    Label lbl = new() { Text = ligne.Nom, Left = 20, Top = y };
                    Controls.Add(lbl);

                    int x = 150;
                    foreach (var poste in ligne.Postes)
                    {
                        Panel p = new() { Width = 50, Height = 50, Left = x, Top = y };
                        p.Paint += (s, e) =>
                            e.Graphics.FillEllipse(
                                new SolidBrush(CouleurEtat(poste.Etat)), 0, 0, 48, 48);

                        Controls.Add(p);
                        affichagePostes[poste] = p;

                        Button defaut = new()
                        {
                            Text = "Défaut",
                            Left = x - 10,
                            Top = y + 60,
                            Width = 70
                        };
                        defaut.Click += (s, e) =>
                            opc.WriteNodeValue(poste.NodeIdEtat, (int)EtatPoste.Defaut);

                        Controls.Add(defaut);
                        x += 150;
                    }
                    y += 150;
                }
            }

            private void InitialiserTimer()
            {
                timer = new System.Windows.Forms.Timer { Interval = 2000 };
                timer.Tick += (s, e) => RafraichirEtats();
                timer.Start();
            }

            private void RafraichirEtats()
            {
                foreach (var ligne in lignes)
                    foreach (var poste in ligne.Postes)
                    {
                        var ancien = poste.Etat;
                        string valeurStr = opc.ReadValue(poste.NodeIdEtat);

                        if (!int.TryParse(valeurStr, out int valeurOPC))
                        {
                            poste.Etat = EtatPoste.SansEtat;
                            return;
                        }

                        poste.Etat = valeurOPC switch
                        {
                            0 => EtatPoste.SansEtat,
                            1 => EtatPoste.Auto,
                            2 => EtatPoste.Attente,
                            3 => EtatPoste.Defaut,
                            4 => EtatPoste.Bourrage,
                            _ => EtatPoste.SansEtat
                        };

                        if (ancien != poste.Etat)
                        {
                            HistoriqueService.Sauvegarder(poste, ancien, poste.Etat);
                            Logger.Log($"État {poste.Nom} : {ancien} → {poste.Etat}");
                        }

                        affichagePostes[poste].Invalidate();
                    }
            foreach (var poste in nodeIdsValeursPostes)
            {
                MettreAJourValeurPosteDepuisOPC(poste.Key);
            }
        }

            private Color CouleurEtat(EtatPoste e) => e switch
            {
                EtatPoste.Auto => Color.Green,
                EtatPoste.Attente => Color.Yellow,
                EtatPoste.Defaut => Color.Red,
                EtatPoste.Bourrage => Color.Orange,
                _ => Color.Gray
            };
        }
}

