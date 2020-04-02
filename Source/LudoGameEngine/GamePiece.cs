using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    class GamePiece
    {
        public int GamePieceID { get; set; }
        public int CurrentPos { get; set; } = 0;
        public int LocalStartPos { get; set; } = 0;
        public bool PieceFinished { get; set; }

        IList<bool> LocalCoordinatePositions = new List<bool>();

        public GamePiece(int id)
        {
            this.GamePieceID = id;
        }

        private void InitializeLocalPositions()
        {
            for(int i = 0; i < 46; i++)
            {
                if(i !=0)
                    LocalCoordinatePositions.Add(false);
            }
        }
    }
}
