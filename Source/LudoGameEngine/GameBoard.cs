using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GameBoard
    {
        IList<BoardCoordinates> CoordinateOuterPosition = new List<BoardCoordinates>();
    }

    public class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        public string Color { get; set; }
        public int PieceID { get; set; }
    }
}
