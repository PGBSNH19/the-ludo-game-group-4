using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    class GamePlayer
    {


        IList<GamePiece> pieces = new List<GamePiece>();

        public GamePlayer(int id, string color)

        private void InitializePiece()
        {
            for(int i = 1; i <= 4; i++)
            {
                pieces.Add(new GamePiece(i));
            }
        } 
    }
}
