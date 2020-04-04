using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GamePiece
    {
        public int PieceID { get; set; }
        public int CurrentPos { get; set; } = 0;
        public int LocalStartPos { get; set; } = 0;
        public bool PieceInGoal { get; set; } = false;

        IList<bool> LocalCoordinatePositions = new List<bool>();

        public GamePiece(int id)
        {
            this.PieceID = id;
        }

        private void InitializeLocalPositions()
        {
            for(int i = 0; i <= 45; i++)
            {
                if(i !=0)
                    LocalCoordinatePositions.Add(false);
            }
        }
    }
}
