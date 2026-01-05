using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace FRONT
    {
        public class FormLegende : Form
        {
            public FormLegende()
            {
                Text = "Légende des états MES";
                Width = 400;
                Height = 350;
                StartPosition = FormStartPosition.CenterParent;
                BackColor = Color.White;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;

                Label titre = new Label
                {
                    Text = "Légende des états des postes",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Top = 20,
                    Left = 60
                };
                Controls.Add(titre);

                int y = 70;

                AjouterLigne(Color.Green, "Poste en automatique", y); y += 40;
                AjouterLigne(Color.Yellow, "Poste en attente", y); y += 40;
                AjouterLigne(Color.Red, "Poste en défaut", y); y += 40;
                AjouterLigne(Color.Purple, "Poste en bourrage", y); y += 40;
                AjouterLigne(Color.Gray, "Poste sans état", y);

                Button btnFermer = new Button
                {
                    Text = "Fermer",
                    Width = 100,
                    Height = 30,
                    Top = 260,
                    Left = 140
                };
                btnFermer.Click += (s, e) => Close();
                Controls.Add(btnFermer);
            }

            private void AjouterLigne(Color couleur, string texte, int y)
            {
                Panel cercle = new Panel
                {
                    Width = 25,
                    Height = 25,
                    Left = 40,
                    Top = y
                };

                cercle.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(new SolidBrush(couleur), 0, 0, 23, 23);
                };

                Label label = new Label
                {
                    Text = texte,
                    Left = 80,
                    Top = y + 3,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10)
                };

                Controls.Add(cercle);
                Controls.Add(label);
            }
        }
    }

