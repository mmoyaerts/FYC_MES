using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lien_POO_BDD.Models
{
    public class Ligne
    {
        public long id { get; }

        public string name { get; set; }

        public List<Poste> postes { get; set; }

        public Ligne(string name) {

            this.name = name;
        }

    }
}
