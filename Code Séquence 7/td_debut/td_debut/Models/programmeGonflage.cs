using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td_debut.Models
{
    public class programmeGonflage
    {
        public int IdGonflage { get; set; }

        public string LabelGonflage { get; set; }

        public programmeGonflage(int idGonflage, string labelGonflage)
        {
            IdGonflage = idGonflage;
            LabelGonflage = labelGonflage;
        }

        public override string ToString()
        {
            return LabelGonflage;
        }
    }
}
