using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GamePlayer
    {
        public int GamePlayerID { get; set; }       
        public string Name { get; set; }
        public string Color { get; set; }
        public int GlobalStartPos { get; set; }

        IList<GamePiece> pieces = new List<GamePiece>();

        public GamePlayer(int id, string name, string color)
        {
            this.GamePlayerID = id;
            this.Name = name;
            this.Color = color;
        }

        private void InitializePiece()
        {
            for(int i = 1; i <= 4; i++)
            {
                pieces.Add(new GamePiece(i));
            }
        } 
    }
}
