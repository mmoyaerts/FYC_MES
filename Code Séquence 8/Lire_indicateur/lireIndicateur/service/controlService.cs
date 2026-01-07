using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lireIndicateur.repository;

namespace lireIndicateur.service
{
    public class controlService
    {
        private readonly controlRepository _controlRepository;

        public controlService()
        {
            _controlRepository = new controlRepository();
        }

        public (double FPY, int nonConformes, int mauvaisPoid, int mauvaiseResi, int mauvaisBouchon) getInfoControl(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                double FPY=0.0;
                var result = _controlRepository.getInfoControl(dateDebut, dateFin);

                // Déstructuration du tuple
                var (total, nonConformes, mauvaisPoid, mauvaiseResi, mauvaisBouchon) = result;

                if (total > 0) {
                    FPY = 1- ((double)nonConformes / total);
                }

                return (FPY, nonConformes, mauvaisPoid, mauvaiseResi, mauvaisBouchon);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des controle." + ex.Message, ex);
            }
        }
    }
}
