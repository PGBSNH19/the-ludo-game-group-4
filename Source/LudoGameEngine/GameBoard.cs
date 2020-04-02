using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GameBoard
    {
        IList<BoardCoordinates> CoordinateOuterPosition = new List<BoardCoordinates>();

        private void MovePiece()
        {

        }
    }

    public class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        
    }
}
