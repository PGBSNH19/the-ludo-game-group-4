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
        public bool PieceFinished { get; set; }
        public Player PlayerId { get; set; }
    }
}
