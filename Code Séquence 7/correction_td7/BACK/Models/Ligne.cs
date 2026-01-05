using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace BACK.Models
{
    public class Ligne
    {
        public string Nom { get; set; }
        public List<Poste> Postes { get; set; } = new();

        public Ligne(string nom)
        {
            Nom = nom;
        }
    }
}
