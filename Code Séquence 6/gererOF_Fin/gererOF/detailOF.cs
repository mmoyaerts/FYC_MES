using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gererOF.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace gererOF
{
    public partial class detailOF : Form
    {
        private readonly OF _of;

        public detailOF(OF of)
        {
            InitializeComponent();
            _of = of;

            // Appelle une méthode pour afficher les infos
            AfficherDetails();
        }
        private void AfficherDetails()
        {
            // Exemple d’affichage dans des labels
            label2.Text = $"{_of.Numero}";
            label6.Text = $"{_of.NumRobotChargement}";
            label7.Text = $"{_of.NumRobotDechargement}";
            label8.Text = $"{_of.NumRobotGonflage}";

            if (_of.ControleGonflage && _of.NumControleGonflage != null)
            {
                radioButton1.Checked = true;
                label9.Text = $"Programme Gonflage : {_of.NumControleGonflage}";
            }
            else
            {
                radioButton1.Checked = false;
                label9.Text = "Aucun contrôle de gonflage";
            }
        }
    }
}
