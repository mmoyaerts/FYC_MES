using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lireIndicateur.repository;

namespace lireIndicateur.service
{
    public class prodService
    {
        private readonly prodRepository _prodRepository;

        public prodService()
        {
            _prodRepository = new prodRepository();
        }

        public (List<(DateTime heure, int totalHeure, int objectif)>, double performance)
            GetNbProductionsParHeure(DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                double performance;
                int totalProd = 0;
                int totalObjectif = 0;
                // Appel du repository
                var result = _prodRepository.GetSommePiecesParHeure(dateDebut, dateFin);
                foreach (var (heure, totalHeure, objectif) in result)
                {
                    totalObjectif += objectif;
                    totalProd += totalHeure;
                }
                performance = (double)totalProd / totalObjectif;

                return (result, performance);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erreur lors de la récupération de la production heure par heure.",
                    ex
                );
            }
        }
    }
}
