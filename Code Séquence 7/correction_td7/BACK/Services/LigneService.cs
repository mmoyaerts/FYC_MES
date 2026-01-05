using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BACK.Models;
using System.Collections.Generic;

namespace BACK.Services
{
    public class LigneService
    {
        public List<Ligne> CreerLignes()
        {
            var ligne1 = new Ligne("Ligne 1")
            {
                Postes =
                {
                    new Poste("Découpe", "ns=3;i=1003"),
                    new Poste("Assemblage", "ns=3;i=1006"),
                    new Poste("Peinture", "ns=3;i=1013")
                }
            };

            var ligne2 = new Ligne("Ligne 2")
            {
                Postes =
                {
                    new Poste("Contrôle", "ns=3;i=1021"),
                    new Poste("Emballage", "ns=3;i=1017"),
                    new Poste("Expédition", "ns=3;i=1024")
                }
            };

            return new List<Ligne> { ligne1, ligne2 };
        }
    }
}

