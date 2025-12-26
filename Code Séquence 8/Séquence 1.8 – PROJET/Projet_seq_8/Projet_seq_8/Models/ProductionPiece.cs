using System;
using System.Globalization;

namespace Projet_seq_8.Models
{
    public class ProductionPiece
    {
        public int IdPiece { get; set; }
        public DateTime DateProd { get; set; }
        public short NbPieces { get; set; }
        public short IdPost { get; set; }
    }
}