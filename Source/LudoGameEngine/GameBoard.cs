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
        private string winner { get; set; } = "";
        private int playerTurn;

        public IGameSession gs;
        public Dice dice = new Dice();

        public enum ColorOrder
        {
            Red,    //0
            Blue,   //1
            Green,  //2
            Yellow  //3
        }


        public GameBoard(IGameSession gameSession)
        {
            this.gs = gameSession;
        }

        //this loop runs the game
        public void GameLoop()
        {            
            InitializeGame();
            
            while (winner != "")
            {
                //all the gameplay here
                


            }

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
            
            playerTurn = DecidePlayerStart(playerAmnt);
        }

        private int DecidePlayerStart(int pAmount)
        {
            
            IDictionary<int, int> playersThrow = new Dictionary<int, int>();
            for(int i = 1; i <= pAmount; i++)
            {
                int dValue = dice.RollDice();
                playersThrow.Add(i, dValue);
            }

            var playerStart = playersThrow.OrderByDescending(x => x.Value).First();
            return playerStart.Value;
        }

        //work in progress
        private void SetPlayOrder(int id, IList<GamePlayer> gp)
        {
            
            var color = gp.Where(c => c.GamePlayerID == id).Select(c => c.Color).FirstOrDefault();

            var values = Enum.GetValues(typeof(ColorOrder));
            var order = new List<int>();
            

            var gpID = gp.Where(g => g.Color == color).Select(g => g.GamePlayerID).FirstOrDefault();
            order.Add(gpID);

            
        }

        private void MovePiece()
        {

        }

        public int SetColorStartPositon(string color)
        {
            int startPos = 0;
            //string playerColor = color;

            if (color == "Red")
                startPos = 1;  //ev. 0 om man räknar från index 0
            else if (color == "Blue")
                startPos = 11;  //ev. 10 om man räknar från index 0
            else if (color == "Green")
                startPos = 21;  //ev. 20 om man räknar från index 0
            else if (color == "Yellow")
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
