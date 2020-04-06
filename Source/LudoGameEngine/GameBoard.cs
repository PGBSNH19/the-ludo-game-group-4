using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LudoGameEngine
{
    public class GameBoard
    {
        public IList<BoardCoordinate> CoordinateOuterPosition = new List<BoardCoordinate>();
        public IList<GamePlayer> GamePlayers = new List<GamePlayer>();

        private int gamePlayerAmnt = 0;
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
            
            while (winner == "")
            {
                //all the gameplay here
                for(int i = 0; i < GamePlayers.Count; i++)
                {
                    //DrawGraphics

                    //dialogue code here
                    Console.WriteLine($"Player {GamePlayers[i].GamePlayerID} {GamePlayers[i].Name} please roll the dice: ");
                    //var pID = GamePlayers[i].GamePlayerID;
                    //gfx code here
                    //player menu code here
                    int diceVal = dice.Roll();
                    Console.WriteLine("Dice rolled: "+ diceVal);
                    int movePieceInGlobalIndex;

                    switch (diceVal)
                    {
                        case 1:                            
                            movePieceInGlobalIndex = RollOne(diceVal, i);
                            CoordinateOuterPosition[movePieceInGlobalIndex].IsOccupied = true;
                            CoordinateOuterPosition[movePieceInGlobalIndex].OccupiedPlayerID = GamePlayers[i].GamePlayerID;
                            //check collission
                            break;
                        case 6:
                            RollSix(diceVal, i);
                            //check collission
                            break;
                        default:
                            movePieceInGlobalIndex = RollRegular(diceVal, i);
                            CoordinateOuterPosition[movePieceInGlobalIndex].IsOccupied = true;
                            CoordinateOuterPosition[movePieceInGlobalIndex].OccupiedPlayerID = GamePlayers[i].GamePlayerID;
                            //check collission
                            break;
                    }

                    //DrawGraphics


                    //dialogue code here 
                    //gfx code here
                    //int selected = input;     //select from menu
                    //bool canMove = false;
                    //for(int y = 0; y < 4; y++)
                    //{
                    //    if (GamePlayers[i].Pieces[y].CurrentPos != GamePlayers[i].Pieces[y].LocalStartPos || steps == 6 || steps == 1)
                    //    {
                    //        canMove = true;                           
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Sorry you cannot move any pieces");
                    //    }
                    //}



                    //if(canMove == true)
                    //{
                    //    Console.WriteLine("Select piece to move: 1, 2, 3, 4");
                    //    Console.Write("Piece selected: ");
                    //    int selected = int.Parse(Console.ReadLine());

                    //    var piece = GamePlayers[i].Pieces.Where(s => s.PieceID == selected).FirstOrDefault();                        
                    //    int gpindex = GamePlayers[i].Pieces.IndexOf(piece);
                    //    GamePlayers[i].Pieces[gpindex].CurrentPos += MovePiece(diceVal);


                    //}




                    //behövs fixas till så  alla spelares pjäser skall vara piece in goal
                    //for (int y = 0; y < 4; y ++)
                    //{
                    //    if (GamePlayers[i].Pieces[y].PieceInGoal == true)
                    //    {
                    //        winner = GamePlayers[i].Name;
                    //    }
                    //}

                }
                


            }

        }
        //GAMEPLAY
        private int MovePiece(int steps)
        {
            //RollOne(steps);
            //RollSix(steps);

            return steps;
        }

        private void CheckCollision(int globalPositionIndex, int playerIndex)
        {
            if(CoordinateOuterPosition[globalPositionIndex].IsOccupied == true)
            {               
                var OtherPlayerID = CoordinateOuterPosition[globalPositionIndex].OccupiedPlayerID;
                var CurrentPlayerID = GamePlayers[playerIndex].GamePlayerID;



                //måste ha metod som hanterar om piece tex gul piece är 0
                if (OtherPlayerID != CurrentPlayerID)
                {
                    KnockOut(OtherPlayerID, globalPositionIndex);
                    CoordinateOuterPosition[globalPositionIndex].IsOccupied = true;
                    CoordinateOuterPosition[globalPositionIndex].OccupiedPlayerID = CurrentPlayerID;
                }                    
                else if(CurrentPlayerID == CurrentPlayerID)
                {
                    //MoveBehind();
                }                   
            }
        }

        private void KnockOut(int playerID, int toLocalPosition)
        {
            int localPosition = toLocalPosition++;
            var player = GamePlayers.Where(p => p.GamePlayerID == playerID).FirstOrDefault();
            int playerIndex = GamePlayers.IndexOf(player);
            var opponentPiece = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos == localPosition).FirstOrDefault();
            int opponentPieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(opponentPiece);

            GamePlayers[playerIndex].Pieces[opponentPieceIndex].CurrentPos = 0;
        }

        private void MoveBehind(int playerID, int localPosition)
        {

        }

        private int RollOne(int stepsToMove, int playerIndex)
        {
            //dialoge here
            foreach(var p in GamePlayers[playerIndex].Pieces)
            {
                Console.Write($"Choose a piece to move: PieceID: {p.PieceID} ");
            }
            Console.WriteLine(" ");

            
            int id = int.Parse(Console.ReadLine());

            var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
            int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);
            int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
            GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
            GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;

            int convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
                       
            return convertToGlobalPosIndex;

            //CheckCollission
            //Draw Graphics
        }

        private void RollSix(int stepsToMove, int playerIndex)
        {
            var moveTwoPieces = GamePlayers[playerIndex].Pieces.Where(s => s.CurrentPos == 0);
            if(moveTwoPieces.Count() >= 2)
            {

                //Visa meny istället
                Console.WriteLine("Please Choose to move 1 piece 6 steps, or two pieces out from nest");
                Console.Write("To choose one piece type: 1, to choose two pieces type: 2 ");
                
                int choice = int.Parse(Console.ReadLine());

                if(choice == 2)
                {
                    for (int i = 0; i < moveTwoPieces.Count(); i++)
                    {
                        Console.Write($"Avaliable pieces to move:");
                        foreach (var p in moveTwoPieces)
                        {
                            Console.Write($"{p.PieceID} ");
                           
                        }
                        Console.WriteLine(" ");

                        Console.WriteLine($"Please enter ID for pice{i+1}:");
                        int id = int.Parse(Console.ReadLine());
                        stepsToMove = 1;
                        
                        var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
                        int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);
                        int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
                        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
                        GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
                        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;
                        int convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
                        CoordinateOuterPosition[convertToGlobalPosIndex].IsOccupied = true;
                        CoordinateOuterPosition[convertToGlobalPosIndex].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
                    }
                }         
                else if(choice == 1)
                {
                    Console.WriteLine("Choose a piece to move");
                    int id = int.Parse(Console.ReadLine());

                    var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
                    int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);
                    int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
                    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
                    GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
                    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;

                    int convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
                    CoordinateOuterPosition[convertToGlobalPosIndex].IsOccupied = true;
                    CoordinateOuterPosition[convertToGlobalPosIndex].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
                }
                
            }


        }

        private int RollRegular(int stepsToMove, int playerIndex)
        {
            //dialoge here
            //menu here ersätt med foreachloop

            var piecesToMove = GamePlayers[playerIndex].Pieces.Where(s => s.CurrentPos != 0);
            int convertToGlobalPosIndex = 0;

            if (piecesToMove.Count() != 0)
            {
                foreach (var p in piecesToMove)
                {
                    Console.WriteLine("Avaliable pieces to move:");
                    Console.Write($"ID:{p.PieceID} ");
                }
                Console.WriteLine("\n\rChoose a piece to move: ");
                int id = int.Parse(Console.ReadLine());

                var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
                int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);

                int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;
                convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
                
            }

            return convertToGlobalPosIndex;
        }

        //INITIALIZATION
        public void InitializeGame()
        {
            //players
            gamePlayerAmnt = gs.GetPlayerAmount();
            IList<Tuple<int, string, string>> sessionData = gs.GetSessionData();

            InitializeBoardCoordinates();

            for (int i = 0; i < gamePlayerAmnt; i++)
            {
                GamePlayers.Add(new GamePlayer(sessionData[i].Item1,sessionData[i].Item2, sessionData[i].Item3));
                GamePlayers[i].GlobalStartPos = SetColorStartPositon(GamePlayers[i].Color);
            }
            
            playerTurn = DecidePlayerStart(GamePlayers.Count);
            GamePlayers = SetPlayOrder(playerTurn, GamePlayers);
        }

        private void InitializeBoardCoordinates()
        {
            for(int i = 0; i <40; i++)
            {
                CoordinateOuterPosition.Add(new BoardCoordinate());
            }
        }
       
        //work in progress
        private int DecidePlayerStart(int pAmount)
        {
            
            IDictionary<int, int> playersThrow = new Dictionary<int, int>();
            for(int i = 1; i <= pAmount; i++)
            {
                int dValue = dice.Roll();
                playersThrow.Add(i, dValue);
            }

            var playerStart = playersThrow.OrderByDescending(x => x.Value).First();
            return playerStart.Key;
        }

        
        public IList<GamePlayer> SetPlayOrder(int id, IList<GamePlayer> gp)
        {  
            var currentColor = gp.Where(c => c.GamePlayerID == id).Select(c => c.Color).FirstOrDefault();
            var nextColor = "";

            var newOrder = new List<GamePlayer>();

            var gPID = gp.Where(g => g.Color == currentColor).Select(g => g).FirstOrDefault();
            newOrder.Add(gPID);
            gp.Remove(gPID);

            var values = Enum.GetNames(typeof(ColorOrder));
            int index = Array.IndexOf(values, currentColor);

            for(int i = 0; i < gp.Count; i++)
            {
                while (currentColor != gp[i].Color)
                {
                    index = Array.IndexOf(values, currentColor);
                    if (index != values.Length)
                    {
                        index++;
                        if (index == values.Length)
                            index = 0;

                        nextColor = values[index];                        
                    }

                    currentColor = nextColor;
                }

                gPID = gp.Where(g => g.Color == currentColor).Select(g => g).FirstOrDefault();
                newOrder.Add(gPID);
            }

            return newOrder;
        }

        public int SetColorStartPositon(string color)
        {
            int startPos = 0;

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

    public class BoardCoordinate
    {
        public bool IsOccupied { get; set; }
        public int BoardPosition { get; set; }
        public int OccupiedPlayerID { get; set; }
    }
}
