using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lireIndicateur.repository;

namespace lireIndicateur.service
{
    public class defautService
    {
        private readonly defautRepository _defautRepository;

        public defautService()
        {
            _defautRepository = new defautRepository();
        }

        public List<(int, int)> getNbrArret(DateTime dateDebut, DateTime dateFin)
        {
            return _defautRepository.getNbrArret(dateDebut, dateFin);
        }

        public List<(int, TimeSpan)> getArretPoste(DateTime dateDebut, DateTime dateFin) {
            return _defautRepository.GetTempsArretParPoste(dateDebut, dateFin);
        }

        public (TimeSpan, double) getArretTotal(DateTime dateDebut, DateTime dateFin) { 
            var tempsArret = _defautRepository.GetTempsArretTotal(dateDebut, dateFin);
            double perf = tempsArret.TotalHours / 3;

            return (tempsArret, perf);
        }

    }
}
