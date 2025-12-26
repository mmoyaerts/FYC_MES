using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_seq_8.Models
{
    public class DefautPoste
    {
        public int Id { get; set; }
        public short IdPost { get; set; }
        public bool Defaut { get; set; }
        public DateTime DateHeure { get; set; }
    }
}