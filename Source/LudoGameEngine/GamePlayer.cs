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
        public int GlobalStartPosition { get; set; }

        public IList<GamePiece> Pieces = new List<GamePiece>();

        public GamePlayer(int id, string name, string color)
        {
            this.GamePlayerID = id;
            this.Name = name;
            this.Color = color;
            InitializePiece();
        }

        private void InitializePiece()
        {
            for(int i = 1; i <= 4; i++)
            {
                Pieces.Add(new GamePiece(i));
            }
        } 
    }
}
