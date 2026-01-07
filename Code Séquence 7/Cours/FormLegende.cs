using System;
using System.Drawing;
using System.Windows.Forms;

namespace FRONT
{
    public partial class FormLegende : Form
    {
        public FormLegende()
        {
            Text = "Légende des postes";
            Width = 600;
            Height = 200;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;

            var items = new (string texte, Color couleur)[]
            {
                ("Poste en auto", Color.Green),
                ("Poste en attente", Color.Yellow),
                ("Défaut", Color.Red),
                ("Poste en bourrage", Color.Purple),
                ("Aucun état", Color.Gray)
            };

            int x = 30;
            foreach (var (texte, couleur) in items)
            {
                Panel rect = new Panel
                {
                    BackColor = couleur,
                    Width = 20,
                    Height = 20,
                    Left = x,
                    Top = 40
                };

                Label lbl = new Label
                {
                    Text = texte,
                    Left = x + 30,
                    Top = 40,
                    Width = 130
                };

                Controls.Add(rect);
                Controls.Add(lbl);
                x += 160;
            }

            Button btnFermer = new Button
            {
                Text = "Fermer",
                Width = 100,
                Height = 35,
                Top = 100,
                Left = 230
            };
            btnFermer.Click += (s, e) => Close();
            Controls.Add(btnFermer);
        }

        private void FormLegende_Load(object sender, EventArgs e)
        {

        }
    }
}