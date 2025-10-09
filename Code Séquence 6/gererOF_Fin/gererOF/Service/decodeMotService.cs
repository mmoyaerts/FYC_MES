using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gererOF.Service
{
    internal class decodeMotService
    {
        /// <summary>
        /// Extrait le numéro de programme de chargement (bits 0..3)
        /// et le numéro de programme de déchargement (bits 4..7) à partir d'un octet (0-255).
        /// </summary>
        public static (int NumChargement, int NumDechargement) ExtraireProgrammesApiChargement(int valeur)
        {
            if (valeur < 0 || valeur > 255)
                throw new ArgumentOutOfRangeException(nameof(valeur), "La valeur doit être comprise entre 0 et 255.");

            // nibble bas -> bits 0..3
            int numChargement = valeur & 0x0F;

            // nibble haut -> bits 4..7 (on décale de 4 vers la droite)
            int numDechargement = (valeur >> 4) & 0x0F;

            return (numChargement, numDechargement);
        }

        public static (int NumProgrammeGonflage, bool AvecControleGonflage, int NumProgrammeControleGonflage) ExtraireInfosControleGonflage(int valeur)
        {
            if (valeur < 0 || valeur > 255)
                throw new ArgumentOutOfRangeException(nameof(valeur), "La valeur doit être comprise entre 0 et 255.");

            // Bits 0..3 : numéro programme gonflage
            int numGonflage = valeur & 0b00001111;

            // Bit 4 : booléen contrôle gonflage
            bool avecControle = (valeur & 0b00010000) != 0;

            // Bits 5..7 : numéro programme contrôle gonflage
            int numControleGonflage = (valeur & 0b11100000) >> 5;

            return (numGonflage, avecControle, numControleGonflage);
        }
    }
}
