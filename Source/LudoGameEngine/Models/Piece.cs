using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LudoGameEngine.Models
{
    public class Piece
    {
        [Key]
        public int PieceID { get; set; }
        public int Position { get; set; }
        public int PlayerPieceID { get; set; }
        public IList<PlayerPiece>PlayerPiece { get; set; }
    }
}
