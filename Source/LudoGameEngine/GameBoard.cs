using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    class GameBoard
    {
        IList<BoardCoordinates> CoordinateOuterPosition = new List<BoardCoordinates>();
    }

    class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        public string Color { get; set; }
        public int PieceID { get; set; }
    }
