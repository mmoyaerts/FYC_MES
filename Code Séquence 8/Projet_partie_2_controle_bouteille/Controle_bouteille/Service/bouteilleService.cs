using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controle_bouteille.Models;
using Controle_bouteille.Repository;
using Dll_OPC;

namespace Controle_bouteille.Service
{
    internal class bouteilleService
    {
        private readonly bouteilleRepository _bouteilleRepository;

        public bouteilleService()
        {
            _bouteilleRepository = new bouteilleRepository();
        }
        public void insererBouteille(OpcUaClientManager opcUaClient)
        {
            try
            {
                int CodeTra = OPCUAService.Lire<int>("ns=3;i=1021", opcUaClient);
                var poid = OPCUAService.Lire<int>("ns=3;i=1022", opcUaClient);
                var presenceBouchon = OPCUAService.Lire<bool>("ns=3;i=1023", opcUaClient);
                var resistance = OPCUAService.Lire<int>("ns=3;i=1024", opcUaClient);

                Bouteille bouteille = new Bouteille(CodeTra, poid, presenceBouchon, resistance);

                _bouteilleRepository.insererBouteille(bouteille);

                Console.WriteLine("bouteille inserer : ", CodeTra);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
