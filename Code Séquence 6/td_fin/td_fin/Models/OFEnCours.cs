using System;
using System.Globalization;

namespace td_fin.Models
{
    public class OfEnCours
    {
        public int Id { get; set; }
        public int NumeroOF { get; set; }
        public int Quantite { get; set; }
        public DateTime DateDeLancement { get; set; }
        public bool IsValid { get; set; }

        // AJOUT : Propriétés pour stocker les programmes (utilisés dans l'OF classique mais liés à l'OF en cours)
        public int ProgrammeChargement { get; set; }
        public int ProgrammeDechargement { get; set; }

        public OfEnCours() { }

        // ... (Autres constructeurs)

        public override string ToString()
        {
            // Mise à jour de l'affichage pour inclure les programmes
            return $"OF : {NumeroOF}\n" +
                   $"Quantité : {Quantite}\n" +
                   $"Lancement : {DateDeLancement.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}\n" +
                   $"Programmes C/D : {ProgrammeChargement}/{ProgrammeDechargement}";
        }
    }
}