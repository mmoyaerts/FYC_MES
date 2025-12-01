using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Ajout des usings de base pour un fichier C# complet

namespace td_fin.Service
{
    internal static class decodeMotService
    {

        // ************************************************************
        // * Exercice 1.2 : Décryptage Programme Chargement           *
        // ************************************************************
        /// Isole et retourne le numéro du Programme Robot Chargement (Bits 0-3) à partir du mot API.
        public static int ExtraireProgrammeChargement(int motApi)
        {
            // Isole les 4 bits de poids faible (Bits 0-3) en utilisant le masque binaire 00001111 (15 en décimal)
            return motApi & 15;
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
        // * Exercice 2 : Encodage (Mise à Jour Définition OF)        *
        // ************************************************************
        /// Combine les numéros de programme de chargement (Bits 0-3) et déchargement (Bits 4-7)
        /// dans un seul entier (octet).
        public static int EncoderProgrammesApiChargement(int numChargement, int numDechargement)
        {
            // Objectif : Mettre numChargement sur les bits 0-3 et numDechargement sur les bits 4-7.

            // 1. Décaler le programme de Déchargement de 4 positions vers la gauche (<< 4)
            //    Ceci le place sur les bits 4-7.
            int dechargementDecale = numDechargement << 4;

            // 2. Combiner les deux valeurs avec un OU binaire logique (OR |)
            //    Le 'OR' garantit que si un bit est à 1 dans l'une ou l'autre des valeurs, il est à 1 dans le résultat.
            int motFinal = dechargementDecale | numChargement;

            return motFinal;
        }
    }
}