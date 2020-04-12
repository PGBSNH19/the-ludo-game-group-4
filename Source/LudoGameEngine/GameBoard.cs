using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using LudoGameEngine.Data;

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

        private int saveGame { get; set; } = 0;

        private ConsoleColor playerTextColor;

        private IGameSession gs;

        private GameData gameData = new GameData();

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
        private int gfxGameBoardPiecePos = 14;
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            if (NewGame == true)
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
            else
            {
                InitializeBoardCoordinates();
            }

            
            //continue gameloop until winner
            while (winner == "")
            {
                //all the gameplay here
                for (int i = 0; i < GamePlayerAmnt; i++)
                {
                    playerTextColor = DrawGFX.BrushColor(GamePlayers[i].Color);

                    /********************************************
                                GFX-POSITIONING STATUSTEXT
                    ********************************************/

                    //header
                    DrawGFX.SetDrawPosition(0, gfxInfoPos);
                    Console.Write(new string("[GAME PROGRESS INFORMATION]").PadRight(35));
                    
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


                    /********************************************
                           GFX-POSITIONING GAME BOARD
                    ********************************************/

                    //positon for Game Board Title
                    DrawGFX.SetDrawPosition(0, gfxGameBoardTitlePos);
                    Console.WriteLine("GAME BOARD");

                    //position for Game Board Pieces
                    DrawGFX.SetDrawPosition(0, gfxGameBoardPiecePos);
                    var commonGameBoardPieces = DrawGFX.CreateBoard(40, BoardGFXItem.GameBoardPiecesGFX);

                    List<Tuple<int, int, string>> tmp = new List<Tuple<int, int, string>>();
                    for (int y = 0; y < CoordinateOuterPosition.Count; y++)
                    {
                        if (CoordinateOuterPosition[y].IsOccupied == true)
                        {
                            var color = GamePlayers.Where(c => c.GamePlayerID == CoordinateOuterPosition[y].OccupiedPlayerID).Select(c => c.Color).FirstOrDefault();
                            tmp.Add(Tuple.Create(y, CoordinateOuterPosition[y].OccupiedPlayerID, color));
                        }
                    }                    
                    commonGameBoardPieces = DrawGFX.RenderGameBoardPieces(commonGameBoardPieces, tmp);

                    //position for Game Board
                    DrawGFX.SetDrawPosition(0, gfxGameBoardPos);
                    var commonGameBoard = DrawGFX.CreateBoard(40,BoardGFXItem.GameBoardGFX);
                    commonGameBoard = DrawGFX.RenderGameBoard(commonGameBoard);


                   /********************************************
                            GFX-POSITIONING PLAYER-RELATED
                   ********************************************/

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
                    playerBoard1[piece1.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece1.CurrentPos);
                    playerBoard1 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard1);

                    //position piece board 2
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard2Pos);
                    var playerBoard2 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 2]: ").PadRight(10));

                    //update piece gfx and render pieceboard 2
                    playerBoard2[piece2.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece2.CurrentPos);
                    playerBoard2 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard2);

                    //position piece board 3
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard3Pos);
                    var playerBoard3 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 3]: ").PadRight(10));

                    //update piece gfx and render pieceboard 3
                    playerBoard3[piece3.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece3.CurrentPos);
                    playerBoard3 = DrawGFX.RenderPieceBoard(GamePlayers[i].Color, playerBoard3);

                    //position piece board 4
                    DrawGFX.SetDrawPosition(0, gfxPieceBoard4Pos);
                    var playerBoard4 = DrawGFX.CreateBoard(46, BoardGFXItem.PieceBoardGFX);
                    Console.Write(new string("[Piece 4]: ").PadRight(10));

                    //update piece gfx and render pieceboard 4
                    playerBoard4[piece4.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece4.CurrentPos);
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

                    
                    /********************************************
                                    DICE-RELATED
                    ********************************************/

                    //position for dice btn                    
                    DrawGFX.SetDrawPosition(0, gfxInteractablePos);
                    int diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");                   

                    //position for dice roll text
                    DrawGFX.SetDrawPosition(0, gfxResultPos);
                    Console.WriteLine("Dice rolled: "+ diceValue);

                    //dice-roll
                    IList<string> options = new List<string>();
                    int selectedPiece = 0;
                    switch (diceValue)
                    {
                        case 1:
                            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                            Console.WriteLine("Choose a piece to move");

                            options = CreatePieceBtnOptions(true, i);
                            if (options.Count() != 0)
                            {
                                selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                                string pieceName = options[selectedPiece];
                                int pieceID = int.Parse(pieceName.Last().ToString());

                                var pieceToMove = GetPieceByID(i, pieceID);
                                //var pieceDice = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();
                                int pieceIndex = GetPieceIndex(i, pieceToMove);

                                //GamePlayers[i].Pieces[pieceIndex].CurrentGlobalPos = GamePlayers[i].GlobalStartPos + GamePlayers[i].Pieces[pieceIndex].CurrentPos;
                                int currentGlobalPosition = GetCurrentGlobalPiecePosition(i, pieceIndex);

                                int newCurrentPosition = UpdateLocalPiecePosition(i, pieceIndex, diceValue);

                                int newGlobalPosition = GetNewGlobalPiecePosition(i, newCurrentPosition);

                                UpdateGlobalPiecePosition(i, currentGlobalPosition, newGlobalPosition);

                                //print positions to screen
                                DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                                Console.WriteLine($"Player {GamePlayers[i].GamePlayerID}, Piece {pieceToMove.PieceID} moved from position {currentGlobalPosition} to position {newGlobalPosition} in Game Board" +
                                    $" and to position {newCurrentPosition} in Player Board");
                            }

                            break;

                        //case 6:
                        //    DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                        //    Console.WriteLine("You rolled 6. Please make a choice:");



                        //    //List<string> moveOptions = new List<string>();
                        //    //var piecesInNest = GamePlayers[i].Pieces.Where(p => p.CurrentPos == p.LocalStartPos);
                        //    //if (piecesInNest.Count() >= 2)
                        //    //{
                        //    //    moveOptions.Add("Move 1 piece 6 steps?");
                        //    //    moveOptions.Add("Move 2 pieces 1 step?");
                        //    //}
                        //    //else 
                        //    //    moveOptions.Add("Move 1 piece 6 steps?");

                        //    List<string> moveOptions = new List<string>() { "Move 1 piece 6 steps ?", "Move 2 pieces 1 step ?" };
                        //    int selectMoveOption = CreateInteractable.OptionMenu(true, moveOptions, 0, gfxInteractablePos);

                        //    DrawGFX.ClearDrawContent(0, gfxInteractableInfoPos);
                            
                        //    if (selectMoveOption == 0)
                        //    {                               
                        //        DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                        //        Console.WriteLine("Choose a piece to move");

                        //        options = CreatePieceBtnOptions(true, i);
                        //        MovePiece(selectedPiece, options, diceValue, i);

                        //        //if (options.Count() != 0)
                        //        //{
                        //        //    var selectedPieceToMove = GetSelectedPieceToMove(selectedPiece, options, i);
                        //        //    MovePiece(selectedPieceToMove, diceValue, i);
                        //        //}

                        //    }
                        //    else
                        //    {                                
                        //        for (int y = 0; y < 2; y++)
                        //        {
                        //            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                        //            Console.WriteLine("Choose a piece to move");

                        //            options = CreatePieceBtnOptions(true, i);
                        //            MovePiece(selectedPiece, options, 1, i);

                        //            //options = CreatePieceBtnOptions(true, i);
                        //            //if (options.Count() != 0)
                        //            //{
                        //            //    var selectedPieceToMove = GetSelectedPieceToMove(selectedPiece, options, i);
                        //            //    MovePiece(selectedPieceToMove, 1, i);
                        //            //}

                        //        }                             
                        //    }
                        //    break;

                        default:
                            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                            Console.WriteLine("Choose a piece to move");

                            options = CreatePieceBtnOptions(false, i);
                            if (options.Count() != 0)
                            {
                                selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                                string pieceName = options[selectedPiece];
                                int pieceID = int.Parse(pieceName.Last().ToString());

                                var pieceToMove = GetPieceByID(i, pieceID);
                                //var pieceDice = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();
                                int pieceIndex = GetPieceIndex(i, pieceToMove);

                                //GamePlayers[i].Pieces[pieceIndex].CurrentGlobalPos = GamePlayers[i].GlobalStartPos + GamePlayers[i].Pieces[pieceIndex].CurrentPos;
                                int currentGlobalPosition = GetCurrentGlobalPiecePosition(i, pieceIndex);

                                int newCurrentPosition = UpdateLocalPiecePosition(i, pieceIndex, diceValue);

                                int newGlobalPosition = GetNewGlobalPiecePosition(i, newCurrentPosition);

                                UpdateGlobalPiecePosition(i, currentGlobalPosition, newGlobalPosition);

                                //print positions to screen
                                DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                                Console.WriteLine($"Player {GamePlayers[i].GamePlayerID}, Piece {pieceToMove.PieceID} moved to position {currentGlobalPosition} from position {newGlobalPosition} in Game Board" +
                                    $" and to position {newCurrentPosition} in Player Board");
                            }
                            break;
                    }

                    
                    /********************************************
                                    RE-RENDER GFX
                    ********************************************/
                    
                    ////update piece gfx new position after movement
                    playerBoard1[piece1.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece1.CurrentPos);
                    playerBoard2[piece2.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece2.CurrentPos);
                    playerBoard3[piece3.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece3.CurrentPos);
                    playerBoard4[piece4.CurrentPos] = DrawGFX.UpdatePlayerPieceGFXByPosition(piece4.CurrentPos);

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


                    /********************************************
                                    GFX-CLEANUP
                    ********************************************/

                    //cleaning up console before next player
                    DrawGFX.ClearDrawContent(0, gfxSubInfoPos);
                    DrawGFX.ClearDrawContent(0, gfxInteractableInfoPos);
                    DrawGFX.ClearDrawContent(0, gfxResultPos);


                    //while (diceValue == 6)
                    //{
                    //    DrawGFX.SetDrawPosition(0, 8);
                    //    diceValue = CreateInteractable.SingleButton(dice.Roll, "Roll");
                    //}

                    //Winner
                    var allPiecesFinished = GamePlayers[i].Pieces.All(p => p.PieceInGoal == true && p.CurrentPos == p.GoalPos);
                    if (allPiecesFinished == true)
                    {
                        winner = GamePlayers[i].Name;                       
                    }

                }

                

                //Save Game or skip
                string[] saveOptions = { "Skip", "Save Game?" };
                saveGame = CreateInteractable.OptionMenu(true, saveOptions, 40, gfxInfoPos);
                if (saveGame == 1)
                {
                    SaveGame();
                }

            }

            SaveWinner();
            DisplayWinner();

        }

        /*===========================================================
                        GAMEPLAY-METHODS
        ===========================================================*/       


        /*------------------DATABASE-RELATED-----------------------*/
        private void SaveGame()
        {
            for(int i = 0; i < GamePlayers.Count; i++)
            {
                for(int y= 0; y < GamePlayers[i].Pieces.Count; y++)
                {
                    gameData.UpdatePiecePosition(SessionName, GamePlayers[i].Name, GamePlayers[i].Pieces[y].PieceID, GamePlayers[i].Pieces[y].CurrentPos);
                }
            }
        }

        private void SaveWinner()
        {
            if(winner != "")
            {
                gameData.UpdateWinner(SessionName, true, "winnername");
            }            
        }

        /*------------------WINNINER-RELATED-----------------------*/
        private void DisplayWinner()
        {
            Console.Clear();
            DrawGFX.SetDrawPosition(50, 8);
            Console.WriteLine("CONGRATULATIONS!!!!");
            DrawGFX.SetDrawPosition(50, 8);
            Console.WriteLine("Winner is: " + winner + "!");
        }



        /*---------------DICE-RELATED-POSITIONING-------------------*/
        ////return the selected piece player has choosed to move
        //private GamePiece GetSelectedPieceToMove(int selectedPiece, IList<string> options, int playerIndex)
        //{
        //        selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
        //        string pieceName = options[selectedPiece];
        //        int pieceID = int.Parse(pieceName.Last().ToString());

        //        var pieceToMove = GetPieceByID(playerIndex, pieceID);

        //        return pieceToMove;
        //}

        ////Move the playerPiece amd write the new position to console
        //private void MovePiece(GamePiece pieceToMove, int playerIndex, int diceValue)
        //{
        //    //if (options.Count() != 0)
        //    //{
        //        int currentLocalPosition = UpdateLocalPiecePosition(playerIndex, pieceToMove, diceValue);

        //        int globalPosition = GetGlobalPiecePosition(playerIndex, currentLocalPosition);
        //        UpdateGlobalPiecePosition(playerIndex, globalPosition);

        //        //print positions to screen
        //        DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
        //        Console.WriteLine($"Player {GamePlayers[playerIndex].GamePlayerID}, Piece {pieceToMove.PieceID} moved to position {globalPosition} in Game Board" +
        //            $" and position {currentLocalPosition} in Player Board");
        //    //}
        //}

        private void MovePiece(int selectedPiece, IList<string> options, int diceValue, int playerIndex)
        {
            if (options.Count() != 0)
            {
                selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                string pieceName = options[selectedPiece];
                int pieceID = int.Parse(pieceName.Last().ToString());

                var pieceToMove = GetPieceByID(playerIndex, pieceID);
                //var pieceDice = GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();
                int pieceIndex = GetPieceIndex(playerIndex, pieceToMove);
                int currentLocalPosition = UpdateLocalPiecePosition(playerIndex, pieceIndex, diceValue);
                int globalPosition = GetNewGlobalPiecePosition(playerIndex, currentLocalPosition);

                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos = globalPosition;



                //UpdateGlobalPiecePosition(playerIndex, globalPosition);

                //print positions to screen
                DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
                Console.WriteLine($"Player {GamePlayers[playerIndex].GamePlayerID}, Piece {pieceToMove.PieceID} moved to position {globalPosition} in Game Board" +
                    $" and position {currentLocalPosition} in Player Board");
            }
        }

        //Player hit 6 with dice
        //private GamePiece MoveSix(int selectMoveOption, int diceValue, int playerIndex)
        //{
        //    IList<string> options = new List<string>();

        //    if (selectMoveOption == 0)
        //    {
        //        options = CreatePieceBtnOptions(true, playerIndex);
        //        int selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
        //        string pieceName = options[selectedPiece];
        //        int pieceID = int.Parse(pieceName.Last().ToString());

        //        MovePiece()
        //    }
        //    else
        //    {

        //    }
        //}

        private void CheckCollision(int globalPosition, int playerIndex)
        {
            if(CoordinateOuterPosition[globalPosition].IsOccupied == true)
            {               
                var otherPlayerID = CoordinateOuterPosition[globalPosition].OccupiedPlayerID;
                var currentPlayerID = GamePlayers[playerIndex].GamePlayerID;

                //måste ha metod som hanterar om piece tex gul piece är 0
                if (otherPlayerID != currentPlayerID)
                {
                    KnockOut(otherPlayerID, globalPosition);
                    CoordinateOuterPosition[globalPosition].IsOccupied = true;
                    CoordinateOuterPosition[globalPosition].OccupiedPlayerID = currentPlayerID;
                }
                //fixa
                else if(otherPlayerID == currentPlayerID)
                {
                    MoveBehind(currentPlayerID, globalPosition);
                }                   
            }
        }

        //KnockOut other player
        private void KnockOut(int playerID, int toLocalPosition)
        {
            int localPosition = toLocalPosition++;
            var player = GamePlayers.Where(p => p.GamePlayerID == playerID).FirstOrDefault();
            int playerIndex = GamePlayers.IndexOf(player);
            var opponentPiece = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos == localPosition).FirstOrDefault();
            int opponentPieceIndex = GamePlayers[playerIndex].Pieces.IndexOf(opponentPiece);

            GamePlayers[playerIndex].Pieces[opponentPieceIndex].CurrentPos = 0;
        }

        //Move behind same player
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

        //Get index of a specific piece
        private int GetPieceIndex(int playerIndex, GamePiece piece)
        {
            return GamePlayers[playerIndex].Pieces.IndexOf(GamePlayers[playerIndex].Pieces.Single( p => p.PieceID == piece.PieceID));
        }

        //Get a specific piece by ID
        private GamePiece GetPieceByID(int playerIndex, int pieceID)
        {
            return GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();
        } 

        //return piece current pos after updated localy
        public int UpdateLocalPiecePosition(int playerIndex, int pieceIndex, int stepsToMove)
        {
            int currentPiecePos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            int goalpos = GamePlayers[playerIndex].Pieces[pieceIndex].GoalPos;
            
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

            return currentPiecePos;
        }

        public int GetCurrentGlobalPiecePosition(int playerIndex, int pieceIndex)
        {
            GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos = GamePlayers[playerIndex].GlobalStartPos + GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
            
            if (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos >= CoordinateOuterPosition.Count - 1)
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos = (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos - (CoordinateOuterPosition.Count - 1));

            return GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos;
        }

        //Convert a piece local position and returns it as global
        public int GetNewGlobalPiecePosition(int playerIndex, int newCurrentPiecePosition)
        {
            int newGlobalPosition = GamePlayers[playerIndex].GlobalStartPos + newCurrentPiecePosition;

            //check if position is out of range. then sets pos at beginning of board.
            //hence in reality its a round board players can go around
            if (newGlobalPosition >= CoordinateOuterPosition.Count-1)
                newGlobalPosition = (newGlobalPosition - (CoordinateOuterPosition.Count - 1));

            return newGlobalPosition;
        }

        //Updates the global board position with what player has currently occupied it
        private void UpdateGlobalPiecePosition(int playerIndex, int currentPieceGlobalPosition, int newGlobalPosition)
        {           
            CoordinateOuterPosition[currentPieceGlobalPosition].IsOccupied = false;
            CoordinateOuterPosition[newGlobalPosition].IsOccupied = true;
            CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
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

        /*===========================================================
                        INITIALIZE-METHODS
        ===========================================================*/
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
