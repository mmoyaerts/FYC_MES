using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACK.Models
{
    public class Poste
    {
        public string Nom { get; set; }
        public string NodeIdEtat { get; set; }
        public EtatPoste Etat { get; set; }

        public Poste(string nom, string nodeId)
        {
            Nom = nom;
            NodeIdEtat = nodeId;
            Etat = EtatPoste.SansEtat;
        }
    }
}

