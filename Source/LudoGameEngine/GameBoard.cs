using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            IList<Tuple<int, string, string>> sessionData = gs.GetSessionData();

            for(int i = 1; i <= playerAmnt; i++)
            {
                gamePlayers.Add(new GamePlayer(sessionData[i].Item1,sessionData[i].Item2, sessionData[i].Item3));
                gamePlayers[i].GlobalStartPos = SetColorStartPositon(gamePlayers[i].Color);
            }
        }

        private void MovePiece()
        {

        }

        private int SetColorStartPositon(string color)
        {
            //foreach(var gp in gamePlayers)
            //{
            //    if (gp.Color == "Red")
            //        gp.GlobalStartPos = 1;  //ev. 0 om man räknar från index 0
            //    else if(gp.Color == "Blue")
            //        gp.GlobalStartPos = 11;  //ev. 10 om man räknar från index 0
            //    else if (gp.Color == "Green")
            //        gp.GlobalStartPos = 21;  //ev. 20 om man räknar från index 0
            //    else if (gp.Color == "Yellow")
            //        gp.GlobalStartPos = 31;  //ev. 30 om man räknar från index 0
            //}
            int startPos = 0;
            string playerColor = color;

            if (playerColor == "Red")
                startPos = 1;  //ev. 0 om man räknar från index 0
            else if (playerColor == "Blue")
                startPos = 11;  //ev. 10 om man räknar från index 0
            else if (playerColor == "Green")
                startPos = 21;  //ev. 20 om man räknar från index 0
            else if (playerColor == "Yellow")
                startPos = 31;  //ev. 30 om man räknar från index 0

            return startPos;
        }
    }

    public class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        public int BoardPosition { get; set; }
        public int OccupiedPlayerID { get; set; }
    }
}
