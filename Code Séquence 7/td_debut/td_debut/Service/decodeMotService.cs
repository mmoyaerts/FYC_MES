using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Ajout des usings de base pour un fichier C# complet

namespace td_debut.Service
{
    internal static class decodeMotService
    {
        // ************************************************************
        // * Exercice 1.2 : Décryptage Programme Chargement          *
        // ************************************************************
        /// Isole et retourne le numéro du Programme Robot Chargement (Bits 0-3) à partir du mot API.
        public static int ExtraireProgrammeChargement(int motApi)
        {
            // TODO Exercice 1.2 : Utiliser une opération binaire (masque) pour isoler les 4 bits de poids faible.
            throw new NotImplementedException();
        }





        // ************************************************************
        // * Décryptage Programme Déchargement (Bits 4-7)             *
        // ************************************************************

        /// Extrait le numéro de programme de déchargement (bits 4..7)
        /// à partir de l'octet du mot API Chargement/Déchargement.
        public static int ExtraireProgrammeDechargement(int motApi)
        {
            // Décaler de 4 bits vers la droite (>> 4) pour ramener les bits 4-7 en position 0-3,
            // puis masquer avec 15 (0x0F) pour isoler ces 4 bits.
            return (motApi >> 4) & 15;
        }

        // ************************************************************
        // * Exercice 2 : Encodage (Mise à Jour Définition OF)         *
        // ************************************************************
        /// Combine les numéros de programme de chargement (Bits 0-3) et déchargement (Bits 4-7)
        /// dans un seul entier (octet) en utilisant l'opération de décalage et le OU logique.
        public static int EncoderProgrammesApiChargement(int numChargement, int numDechargement)
        {
            // TODO Exercice 2 : Implémenter la logique d'encodage binaire (Bit-shifting et OR).
            // Le programme de Déchargement doit être placé sur les Bits 4-7.
            throw new NotImplementedException();
        }
    }
}