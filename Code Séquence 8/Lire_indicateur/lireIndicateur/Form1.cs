using lireIndicateur.service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lireIndicateur
{
    public partial class Form1 : Form
    {
        public DateTime dateDebut;
        public DateTime dateFin;
        public string equipe;
        private readonly controlService _controlService;
        private readonly prodService _prodService;
        private readonly defautService _defautService;

        private double FPY;
        private double performanceProd;
        private double performanceLigne;

        public Form1()
        {
            InitializeComponent();

            _controlService = new controlService();
            _prodService = new prodService();
            _defautService = new defautService();

            //definit la date à aujourd'hui en dd/mm/yyyy
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Value = DateTime.Today;

            //par défaut, je choisis matin
            comboBox1.SelectedIndex = 0;

            // je créer mes 2 colonnes
            dataGridView1.Columns.Add("ColProduction", "Production");
            dataGridView1.Columns.Add("ColObjectif", "Objectif");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //je recupere les données du formulaire
            dateDebut = dateTimePicker1.Value;
            dateFin = dateTimePicker1.Value;
            equipe = comboBox1.SelectedItem.ToString();

            determinerHeure();

            gererControl();
            gererProd();
            gererDefaut();

            label23.Text = (FPY * performanceLigne * performanceProd).ToString();
        }

        private void determinerHeure()
        {
            if (equipe == "Matin")
            {
                //debut à 10h et fin à 13h
                dateDebut = dateDebut.AddHours(10);
                dateFin = dateFin.AddHours(13);
            }
            else
            {
                //debut à 13h et fin à 16h
                dateDebut = dateDebut.AddHours(13);
                dateFin = dateFin.AddHours(16);
            }

        }

        private void gererControl()
        {
            (FPY, var nonConformes, var mauvaisPoid, var mauvaiseResi, var mauvaisBouchon) = _controlService.getInfoControl(dateDebut, dateFin);

            label17.Text = FPY + " %";
            label18.Text = "Mauvais : " + nonConformes;
            label19.Text = "Bouchon : " + mauvaisBouchon;
            label20.Text = "Poid : " + mauvaisPoid;
            label21.Text = "Resistance : " + mauvaiseResi;
        }

        private void gererProd()
        {
            List<(DateTime heure, int totalHeure, int objectif)> prods;

            (prods, performanceProd) = _prodService.GetNbProductionsParHeure(dateDebut, dateFin);
            label4.Text = performanceProd.ToString() + " %";

            creerTableau(prods);
        }

        private void creerTableau(List<(DateTime heure, int totalHeure, int objectif)> prods)
        {
            dataGridView1.Rows.Clear();

            foreach (var row in prods)
            {
                // Ajouter la ligne avec Production et Objectif
                int index = dataGridView1.Rows.Add(
                    row.totalHeure,
                    row.objectif
                );

                // Afficher l'heure sous la forme "9H"
                dataGridView1.Rows[index].HeaderCell.Value = $"{row.heure.Hour}H";
            }
        }

        private void gererDefaut()
        {
            List<(int poste, int nb)> nbrArret;
            List<(int poste, TimeSpan duree)> dureArret;

            nbrArret = _defautService.getNbrArret(dateDebut, dateFin);
            dureArret = _defautService.getArretPoste(dateDebut, dateFin);
            (var arretLigne, performanceLigne) = _defautService.getArretTotal(dateDebut, dateFin);

            foreach (var row in nbrArret)
            {
                if(row.poste == 1)
                {
                    label10.Text = row.nb.ToString();
                }
                if(row.poste == 2)
                {
                    label11.Text = row.nb.ToString();
                }
                if(row.poste == 3)
                {
                    label12.Text = row.nb.ToString();
                }
            }

            foreach (var row in dureArret)
            {
                if (row.poste == 1)
                {
                    label13.Text = row.duree.TotalHours.ToString();
                }
                if (row.poste == 2)
                {
                    label14.Text = row.duree.TotalHours.ToString();
                }
                if (row.poste == 3)
                {
                    label15.Text = row.duree.TotalHours.ToString();
                }
            }
        }
    }
}
