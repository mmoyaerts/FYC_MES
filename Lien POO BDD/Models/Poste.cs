using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lien_POO_BDD.Models
{
    public class Poste
    {
        public long id { get; }

        public string name { get; set; }

        public string adress_ip { get; set; }

        public long LigneId { get; set; }

        public Poste(string name, string adress_ip, long LigneID) {
        
            this.name = name;
            this.adress_ip = adress_ip;
            this.LigneId = LigneID;
        }

    }
}
