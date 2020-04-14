using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine.Models
{
    public class PlayerPiece
    {
        public int PlayerId { get; set; }
        public Player PlayerRef { get; set; }
        public int PieceId { get; set; }
        public Piece PieceRef { get; set; }
    }
}
