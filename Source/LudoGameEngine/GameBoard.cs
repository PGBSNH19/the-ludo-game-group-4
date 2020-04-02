using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GameBoard
    {
        public IList<BoardCoordinates> CoordinateOuterPosition = new List<BoardCoordinates>();
        public IList<GamePlayer> gamePlayers = new List<GamePlayer>();

        public IGameSession gs;   

        public GameBoard(IGameSession gameSession)
        {
            this.gs = gameSession;
        }

        //this loop runs the game
        public void GameLoop()
        {
            InitializeGame();
        }

        public void InitializeGame()
        {
            //players
            int playerAmnt = gs.GetPlayerAmount();
            for(int i = 1; i <= playerAmnt; i++)
            {
                gamePlayers.Add(new GamePlayer(i, "red", "Samir"));
            }

            //Set player starting position here
            //code here


        }

        private void MovePiece()
        {

        }
    }

    public class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        
    }
}
