using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gererOF.Models
{
    public class OF
    {
        public int Numero { get; set; }

        public int NumRobotChargement { get; set; }

        public int NumRobotGonflage {  get; set; }

        public bool ControleGonflage { get; set; }

        public programmeGonflage? NumControleGonflage { get; set; }

        public int NumRobotDechargement { get; set; }

        public OF(int numero, int numRobotChargement,  int numRobotGonflage, bool controleGonflage,
              programmeGonflage? numControleGonflage, int numRobotDechargement)
        {
            Numero = numero;
            NumRobotChargement = numRobotChargement;
            NumRobotGonflage = numRobotGonflage;
            ControleGonflage = controleGonflage;
            NumControleGonflage = numControleGonflage;
            NumRobotDechargement = numRobotDechargement;
        }
    }
}
