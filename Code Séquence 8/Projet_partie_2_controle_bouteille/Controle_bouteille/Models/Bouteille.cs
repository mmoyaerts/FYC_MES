using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_bouteille.Models
{
    internal class Bouteille
    {
        public int CodeTracabilite { get;}
        public int Poids { get;}
        public bool PresenceBouchon { get;}
        public int ResistanceAir { get;}
        public bool Conforme { get; set; }

        public Bouteille(int codeTracabilite, int poids, bool presenceBouchon, int resistanceAir)
        {
            CodeTracabilite = codeTracabilite;
            Poids = poids;
            PresenceBouchon = presenceBouchon;
            ResistanceAir = resistanceAir;
            Conforme = EstValide();
        }

        public bool EstValide()
        {
            return Poids > 19 && Poids < 51 &&
                   ResistanceAir > 5 && ResistanceAir <17 &&
                   PresenceBouchon;
        }
    }
}
