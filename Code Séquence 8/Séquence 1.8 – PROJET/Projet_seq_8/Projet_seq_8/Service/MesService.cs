using Projet_seq_8.Models;
using Projet_seq_8.Repository;


namespace Projet_seq_8.Services
{
        public class MesService
    {
        private readonly MesRepository _repo = new MesRepository();
        

        private Dictionary<short, int> _lastCounters = new Dictionary<short, int>();
        private Dictionary<short, bool> _lastFaultStates = new Dictionary<short, bool>();

        public void CheckMachine(short idPost, int currentCounter, int currentStatus)
        {
            // 1. Logique Production
            HandleProduction(idPost, currentCounter);

            // 2. Logique Défauts
            HandleFaults(idPost, currentStatus);
        }

        private void HandleProduction(short idPost, int currentCounter)
        {
            if (_lastCounters.TryGetValue(idPost, out int lastValue))
            {
                int delta = currentCounter - lastValue;
                if (delta > 0) _repo.InsertProduction(idPost, (short)delta);
                else if (delta < 0) _repo.InsertProduction(idPost, (short)currentCounter);
            }
            _lastCounters[idPost] = currentCounter;
        }

        private void HandleFaults(short idPost, int currentStatus)
        {
            bool isFault = (currentStatus & 4) != 0;
            if (_lastFaultStates.TryGetValue(idPost, out bool lastFault))
            {
                if (isFault != lastFault) _repo.InsertDefaut(idPost, isFault);
            }
            _lastFaultStates[idPost] = isFault;
        }
    }
}