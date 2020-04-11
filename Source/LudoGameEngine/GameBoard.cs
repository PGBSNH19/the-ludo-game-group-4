using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace LudoGameEngine
{
    public class BoardCoordinate
    {
        public bool IsOccupied { get; set; }
        public int BoardPosition { get; set; }
        public int OccupiedPlayerID { get; set; }
    }

    public class GameBoard
    {
        public IList<BoardCoordinate> CoordinateOuterPosition = new List<BoardCoordinate>();
        public IList<GamePlayer> GamePlayers = new List<GamePlayer>();

        public int GamePlayerAmnt { get; set; } = 0;

        public string SessionName { get; set; } = "";

        public bool NewGame { get; set; }

        public Dice dice = new Dice();

        private string winner { get; set; } = "";

        private ConsoleColor playerTextColor;

        private IGameSession gs;

        /*===========================================================
        GFX ConsolePosition
        ===========================================================*/
        private int gfxInfoPos = 0;
        private int gfxSubInfoPos = 1;
        private int gfxStatusPos = 2;
        private int gfxResultPos = 4;
        private int gfxInteractableInfoPos = 6;
        private int gfxInteractablePos = 8;
        private int gfxDividerPos = 10;

        private int gfxGameBoardTitlePos = 12;
        private int gfxGameBoardPiecePis = 14;
        private int gfxGameBoardPos = 15;
        private int gfxPlayerBoardTitlePos = 19;
        private int gfxPlayerInfoPos = 20;
        private int gfxPieceBoard1Pos = 22;
        private int gfxPieceBoard2Pos = 25;
        private int gfxPieceBoard3Pos = 28;
        private int gfxPieceBoard4Pos = 31;
        /**************************************************************/

        public GameBoard(IGameSession gameSession, bool newGame)
        {
            this.gs = gameSession;
            this.NewGame = newGame;
        }

        //this loop runs the game
        public void GameLoop()
        {
            if(NewGame == true)
            {
                Console.Clear();
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                InitializeGame();

                //decide which player start
                IDictionary<int, int> playersRoll = new Dictionary<int, int>();
                for (int i = 1; i <= GamePlayerAmnt; i++)
                {
                    DrawGFX.SetDrawPosition(0, gfxInfoPos);
                    Console.WriteLine("[GAME PROGRESS INFORMATION]");

                    DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                    Console.WriteLine("Decide which player starts by rolling the dice. Highest number wins");

                    DrawGFX.SetDrawPosition(0, gfxStatusPos);
                    Console.WriteLine($"Player {i} please roll the dice. Press[ENTER] to roll");

                    DrawGFX.SetDrawPosition(0, gfxInteractablePos);
                    int diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");

                    DrawGFX.SetDrawPosition(0, gfxResultPos);
                    Console.WriteLine($"Player {i} rolls: {diceValue}");

                    playersRoll.Add(i, diceValue);
                }

                int playerIDStart = DecidePlayerStart(playersRoll);

                DrawGFX.SetDrawPosition(0, gfxStatusPos);
                Console.WriteLine($"Player {playerIDStart} got the highest number and therefore start. Press[ENTER] to continue ");

                GamePlayers = SetPlayOrder(playerIDStart, GamePlayers);

                Console.ReadKey();

                Console.Clear();
            }           

            //continue gameloop until winner
            while (winner == "")
            {
                //all the gameplay here
                for(int i = 0; i < GamePlayerAmnt; i++)
                {
                    playerTextColor = DrawGFX.BrushColor(GamePlayers[i].Color);


                    //header
                    DrawGFX.SetDrawPosition(0, gfxInfoPos);
                    Console.Write(new string("[GAME PROGRESS INFORMATION]").PadRight(35));
                    Console.WriteLine("Press 9 to Save");

                    //position for dialogue
                    DrawGFX.ClearDrawContent(0, gfxStatusPos);
                    DrawGFX.SetDrawPosition(0, gfxStatusPos);
                    Console.ForegroundColor = playerTextColor;
                    Console.Write($"Player {GamePlayers[i].GamePlayerID}: {GamePlayers[i].Name}");
                    Console.ResetColor();
                    Console.WriteLine(" please roll the dice: ");

                    //position for consoledivider
                    DrawGFX.SetDrawPosition(0, gfxDividerPos);
                    Console.Write("\r\n" + new string('=', Console.WindowWidth) + "\r\n");

                    //positon for Game Board Title
                    DrawGFX.SetDrawPosition(0, gfxGameBoardTitlePos);
                    Console.WriteLine("GAME BOARD");

                    //position for Game Board Pieces
                    DrawGFX.SetDrawPosition(0, gfxGameBoardPiecePis);

                    //position for Game Board
                    DrawGFX.SetDrawPosition(0, gfxGameBoardPos);
                    var commonGameBoard = DrawGFX.CreateBoard(40,BoardGFXItem.GameBoardGFX);
                    commonGameBoard = DrawGFX.RenderGameBoard(commonGameBoard);


                    //REFACTORING NEEDED

                    //playerPieceBoards and playerpieces                                        
                    //IList<IList<string>> playerBoards = new List<IList<string>>();

                    //player pieces
                    var piece1 = GamePlayers[i].Pieces.Where(s => s.PieceID == 1).FirstOrDefault();
                    var piece2 = GamePlayers[i].Pieces.Where(s => s.PieceID == 2).FirstOrDefault();
                    var piece3 = GamePlayers[i].Pieces.Where(s => s.PieceID == 3).FirstOrDefault();
                    var piece4 = GamePlayers[i].Pieces.Where(s => s.PieceID == 4).FirstOrDefault(); 

                    //position for playerboard title
                    DrawGFX.SetDrawPosition(0, gfxPlayerBoardTitlePos);
                    Console.ForegroundColor = playerTextColor;
                    Console.WriteLine("PLAYER BOARD");

                    //position for playerboard info
                    DrawGFX.SetDrawPosition(0, gfxPlayerInfoPos);
                    Console.Write(new string($"Player {GamePlayers[i].GamePlayerID}: {GamePlayers[i].Name}").PadRight(30));
                    Console.Write(new string("▲ = Player piece").PadRight(25));
                    Console.WriteLine("(▲) = Piece in Nest");
                    Console.ResetColor();

                    //position piece board 1
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard1Pos);
                    var playerBoard1 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 1]: ").PadRight(10));

                    //update piece gfx and render pieceboard §
                    playerBoard1[piece1.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece1.CurrentPos);
                    playerBoard1 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard1);

                    //position piece board 2
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard2Pos);
                    var playerBoard2 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 2]: ").PadRight(10));

                    //update piece gfx and render pieceboard 2
                    playerBoard2[piece2.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece2.CurrentPos);
                    playerBoard2 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard2);

                    //position piece board 3
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard3Pos);
                    var playerBoard3 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 3]: ").PadRight(10));

                    //update piece gfx and render pieceboard 3
                    playerBoard3[piece3.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece3.CurrentPos);
                    playerBoard3 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard3);

                    //position piece board 4
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard4Pos);
                    var playerBoard4 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 4]: ").PadRight(10));

                    //update piece gfx and render pieceboard 4
                    playerBoard4[piece4.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece4.CurrentPos);
                    playerBoard4 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard4);

                    

                    //int newPosition = 17;
                    //for (int y = 0; y < 4; y++)
                    //{
                    //    newPosition += 3;
                    //    DrawGFX.SetDrawPosition(0, newPosition);
                    //    playerBoards.Add(DrawGFX.PlayerPieceBoard(GamePlayers[i].Color));                        
                    //}

                    ////position out pieces in the playerboards
                    //playerBoards[0].IndexOf(piece1.CurrentPos);

                    //position for dice btn                    
                    DrawGFX.SetDrawPosition(0, gfxInteractablePos);
                    int diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");                   

                    //position for dice roll text
                    DrawGFX.SetDrawPosition(0, gfxResultPos);
                    Console.WriteLine("Dice rolled: "+ diceValue);

                    IList<string> options = new List<string>();
                    int selectedPiece = 0;
                    //int currentLocalPosition = 0;
                    //int globalPosition = 0;

                    switch (diceValue)
                    {
                        case 1:
                            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                            Console.WriteLine("Choose a piece to move");

                            options = CreatePieceBtnOptions(true, i);
                            selectedPiece = (1 + CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));

                            if (options.Count() != 0)
                            {
                                selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                                string pieceName = options[selectedPiece];
                                int pieceID = int.Parse(pieceName.Last().ToString());

                                var pieceDice = GamePlayers[i].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();

                                int currentLocalPosition = UpdateLocalPiecePosition(i, pieceDice, diceValue);

                                int globalPosition = GetGlobalPiecePosition(i, currentLocalPosition);
                                UpdateGlobalPiecePosition(i, globalPosition);
                                DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                                Console.WriteLine($"Player {GamePlayers[i].GamePlayerID}, Piece {pieceDice.PieceID} moved to position {globalPosition} in Game Board" +
                                    $" and position {currentLocalPosition} in Player Board");
                            }
                            break;
                        //case 6:
                            //while (diceValue == 6)
                            //{
                            //    RollSix(diceValue, i);
                            //    diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");
                            //}
                            // CheckCollision(GamePlayers[i].GamePlayerID, movePieceInGlobalIndex);
                            //break;
                        default:
                            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                            Console.WriteLine("Choose a piece to move");

                            options = CreatePieceBtnOptions(false, i);
                           
                            if(options.Count() != 0)
                            {
                                selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                                string pieceName = options[selectedPiece];
                                int pieceID = int.Parse(pieceName.Last().ToString());

                                var pieceDice = GamePlayers[i].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();

                                int currentLocalPosition = UpdateLocalPiecePosition(i, pieceDice, diceValue);

                                int globalPosition = GetGlobalPiecePosition(i, currentLocalPosition);
                                UpdateGlobalPiecePosition(i, globalPosition);
                                DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                                Console.WriteLine($"Player {GamePlayers[i].GamePlayerID}, Piece {pieceDice.PieceID} moved to position {globalPosition} in Game Board" +
                                    $" and position {currentLocalPosition} in Player Board");     
                            }                            
                            break;
                    }

                    //update piece gfx new position after movement
                    playerBoard1[piece1.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece1.CurrentPos);
                    playerBoard2[piece2.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece2.CurrentPos);
                    playerBoard3[piece3.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece3.CurrentPos);
                    playerBoard4[piece4.CurrentPos] = UpdatePlayerPieceGFXByPosition(piece4.CurrentPos);

                    //re-render piece boards
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard1Pos);
                    Console.Write(new string("[Piece 1]: ").PadRight(10));
                    playerBoard1 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard1);

                    DrawGFX.SetDrawPosition(0, gfxPieceBoard2Pos);
                    Console.Write(new string("[Piece 2]: ").PadRight(10));
                    playerBoard2 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard2);

                    DrawGFX.SetDrawPosition(0, gfxPieceBoard3Pos);
                    Console.Write(new string("[Piece 3]: ").PadRight(10));
                    playerBoard3 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard3);

                    DrawGFX.SetDrawPosition(0, gfxPieceBoard4Pos);
                    Console.Write(new string("[Piece 4]: ").PadRight(10));
                    playerBoard4 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard4);


                    //re-render GameBoard

                    //re-render GameBoard Piece

                    //input to player to press enter
                    DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                    Console.WriteLine("Please press [ENTER] to continue");
                    Console.ReadKey();

                    //cleaning up console before next player
                    DrawGFX.ClearDrawContent(0, gfxSubInfoPos);
                    DrawGFX.ClearDrawContent(0, gfxInteractableInfoPos);
                    DrawGFX.ClearDrawContent(0, gfxResultPos);


                    //while (diceValue == 6)
                    //{
                    //    DrawGFX.SetDrawPosition(0, 8);
                    //    diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");
                    //}


                    /*for (int y = 0; y < 4; y++)
                    {
                        if(GamePlayers[i].Pieces[y].CurrentPos == 44)
                        {
                            GamePlayers[i].Pieces[y].PieceInGoal = true;
                        }
                    }

                    var allPiecesFinished = GamePlayers[i].Pieces.All(p => p.PieceInGoal == true);*/

                }

}

        }
        //GAMEPLAY

        private string UpdatePlayerPieceGFXByPosition(int position)
        {
            string pieceGFX = DrawGFX.PieceInNest;
            if(position != 0)
            {
                pieceGFX = DrawGFX.PieceOnBoard;
            }

            return pieceGFX;

        }

        private int MovePiece(int selectedPiece)
        {
            //RollOne(steps);
            //RollSix(steps);

           return selectedPiece;
        }

        

        private void CheckCollision(int globalPositionIndex, int playerIndex)
        {
            if(CoordinateOuterPosition[globalPositionIndex].IsOccupied == true)
            {               
                var otherPlayerID = CoordinateOuterPosition[globalPositionIndex].OccupiedPlayerID;
                var currentPlayerID = GamePlayers[playerIndex].GamePlayerID;

                //måste ha metod som hanterar om piece tex gul piece är 0
                if (otherPlayerID != currentPlayerID)
                {
                    KnockOut(otherPlayerID, globalPositionIndex);
                    CoordinateOuterPosition[globalPositionIndex].IsOccupied = true;
                    CoordinateOuterPosition[globalPositionIndex].OccupiedPlayerID = currentPlayerID;
                }
                //fixa
                else if(otherPlayerID == currentPlayerID)
                {
                    MoveBehind(currentPlayerID, globalPositionIndex);
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

        private void MoveBehind(int playerID, int toLocalPosition)
        {
            int localPosition = toLocalPosition++;
            var player = GamePlayers.Where(p => p.GamePlayerID == playerID).FirstOrDefault();
            int playerIndex = GamePlayers.IndexOf(player);
            var opponentPiece = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos == localPosition).FirstOrDefault();
            int opponentPieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(opponentPiece);

            if(localPosition !=1)
                GamePlayers[playerIndex].Pieces[opponentPieceIndex].CurrentPos = localPosition-1;
        }

        private void RollOne(int stepsToMove, int playerIndex, int id)
        {

        }

        //get index of a specific piece
        private int GetPieceIndex(int playerIndex, GamePiece piece)
        {
            return GamePlayers[playerIndex].Pieces.IndexOf(GamePlayers[playerIndex].Pieces.Single( p => p.PieceID == piece.PieceID));
        }

        //return piece current pos after updated localy
        public int UpdateLocalPiecePosition(int playerIndex, GamePiece piece, int stepsToMove)
        {
            int pieceIndex = GetPieceIndex(playerIndex, piece);
            int currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            int goalpos = GamePlayers[playerIndex].Pieces[pieceIndex].GoalPos;



            //GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = false;
            //GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += stepsToMove;
            //currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            //GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = true;
            
            if ((GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos + stepsToMove) >= goalpos)
            {
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = false;
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos = goalpos;
                currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions.Count-1] = true;
                GamePlayers[playerIndex].Pieces[pieceIndex].PieceInGoal = true;
            }
            else if((GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos + stepsToMove) < goalpos)
            {
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = false;
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += stepsToMove;
                currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = true;
            }


            //if ((GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos + stepsToMove) < 46)
            //{
            //    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = false;
            //    GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += stepsToMove;
            //    currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            //    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = true;
            //}
            //else if((GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos + stepsToMove) >= goalpos)
            //{
            //    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = false;
            //    GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos = goalpos;
            //    GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPiecePos] = true;
            //    GamePlayers[playerIndex].Pieces[pieceIndex].PieceInGoal = true;
            //}


            return currentPiecePos;
        }

        //Convert a piece local position and returns it as global
        public int GetGlobalPiecePosition(int playerIndex, int currentPiecePosition)
        {
            int newGlobalPosition = GamePlayers[playerIndex].GlobalStartPos + currentPiecePosition;

            //check if position is out of range. then sets pos at beginning of board.
            //hence in reality its a round board players can go around
            if (newGlobalPosition >= CoordinateOuterPosition.Count-1)
                newGlobalPosition = (newGlobalPosition - (CoordinateOuterPosition.Count - 1));

            return newGlobalPosition;
        }

        //Updates the global board position with what player has currently occupied it
        private void  UpdateGlobalPiecePosition(int playerIndex, int globalPosition)
        {
            CoordinateOuterPosition[globalPosition].IsOccupied = true;
            CoordinateOuterPosition[globalPosition].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
        }

        private void RollSix(int stepsToMove, int playerIndex)
        {
            //var moveTwoPieces = GamePlayers[playerIndex].Pieces.Where(s => s.CurrentPos == 0);
            //if(moveTwoPieces.Count() >= 2)
            //{

            //    //Visa meny istället
            //    Console.WriteLine("Please Choose to move 1 piece 6 steps, or two pieces out from nest");
            //    Console.Write("To choose one piece type: 1, to choose two pieces type: 2 ");
                
            //    int choice = int.Parse(Console.ReadLine());

            //    if(choice == 2)
            //    {
            //        for (int i = 0; i < moveTwoPieces.Count(); i++)
            //        {
            //            Console.Write($"Avaliable pieces to move:");
            //            foreach (var p in moveTwoPieces)
            //            {
            //                Console.Write($"{p.PieceID} ");
                           
            //            }
            //            Console.WriteLine(" ");

            //            Console.WriteLine($"Please enter ID for pice{i+1}:");
            //            int id = int.Parse(Console.ReadLine());
            //            stepsToMove = 1;
                        
            //            var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
            //            int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);
            //            int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            //            GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
            //            GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
            //            GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;
            //            int convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
            //            CoordinateOuterPosition[convertToGlobalPosIndex].IsOccupied = true;
            //            CoordinateOuterPosition[convertToGlobalPosIndex].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
            //        }
            //    }         
            //    else if(choice == 1)
            //    {
            //        Console.WriteLine("Choose a piece to move");
            //        foreach (var p in moveTwoPieces)
            //        {
            //            Console.Write($"{p.PieceID} ");
            //        }
            //        int id = int.Parse(Console.ReadLine());

            //        var piece = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == id).FirstOrDefault();
            //        int pieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(piece);
            //        int currentPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            //        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = false;
            //        GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += MovePiece(stepsToMove);
            //        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentPos] = true;

            //        int convertToGlobalPosIndex = (GamePlayers[playerIndex].GlobalStartPos) + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--);
            //        if (convertToGlobalPosIndex > CoordinateOuterPosition.Count)
            //            convertToGlobalPosIndex -= (1 - CoordinateOuterPosition.Count);
            //        CoordinateOuterPosition[convertToGlobalPosIndex].IsOccupied = true;
            //        CoordinateOuterPosition[convertToGlobalPosIndex].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
            //    }
                
            //}


        }

        //Return a list of avaliable pieces to move, excluding pieces in goal position
        private IList<string> CreatePieceBtnOptions(bool displayInNest, int index)
        {
            IList<string> pieceOptions = new List<string>();
            
            if(displayInNest == true)
            {
                var piecesTrue = GamePlayers[index].Pieces.Where(p => p.CurrentPos != p.GoalPos);

                foreach (var id in piecesTrue)
                {
                    pieceOptions.Add($"Piece {id.PieceID}");
                }
            }
            else if (displayInNest == false)
            {
                var piecesFalse = GamePlayers[index].Pieces.Where(p => p.CurrentPos != p.LocalStartPos && p.CurrentPos != p.GoalPos);
                if(piecesFalse.Count() != 0)
                {
                    foreach (var id in piecesFalse)
                    {
                        pieceOptions.Add($"Piece {id.PieceID}");
                    }                  
                }
            }         
                           
            return pieceOptions;
        }

        private void RollRegular(int stepsToMove, int playerIndex)
        {
            
        }

        //INITIALIZATION
        public void InitializeGame()
        {
            //players
            GamePlayerAmnt = gs.GetPlayerAmount();
            IList<Tuple<int, string, string>> sessionData = gs.GetSessionData();

            InitializeBoardCoordinates();

            for (int i = 0; i < GamePlayerAmnt; i++)
            {
                GamePlayers.Add(new GamePlayer(sessionData[i].Item1,sessionData[i].Item2, sessionData[i].Item3));
                GamePlayers[i].GlobalStartPos = SetColorStartPositon(GamePlayers[i].Color);
            }
                       
        }

        private void InitializeBoardCoordinates()
        {
            for(int i = 0; i <40; i++)
            {
                CoordinateOuterPosition.Add(new BoardCoordinate());
            }
        }
       
        private int DecidePlayerStart(IDictionary<int,int> playersRoll)
        {               
            var playerStart = playersRoll.OrderByDescending(x => x.Value).First();
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

            var values = Enum.GetNames(typeof(GameColors));
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
                startPos = 0;  
            else if (color == "Blue")
                startPos = 10;  
            else if (color == "Green")
                startPos = 20;  
            else if (color == "Yellow")
                startPos = 30;  

           return startPos;
        }
    }
}
