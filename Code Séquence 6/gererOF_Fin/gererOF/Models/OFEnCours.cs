using System;

namespace gererOF.Models
{
    public class OfEnCours
    {
        public int Id { get; set; }
        public int NumeroOF { get; set; }
        public int Quantite { get; set; }
        public DateTime DateDeLancement { get; set; }
        public bool IsValid { get; set; }

        public OfEnCours() { }

        public OfEnCours(int numeroOF, int quantite, DateTime dateDeLancement, bool isValid)
        {
            NumeroOF = numeroOF;
            Quantite = quantite;
            DateDeLancement = dateDeLancement;
            IsValid = isValid;
        }

        public override string ToString()
        {
            return $"OF : {NumeroOF}\n" +
                   $"Quantité : {Quantite}\n" +
                   $"Lancement : {DateDeLancement:dd/MM/yyyy HH:mm}";
        }

    }
}
