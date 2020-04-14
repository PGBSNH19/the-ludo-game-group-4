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
        public int OccupiedPlayerPieceID { get; set; }
    }

    public class GameBoard
    {
        public IList<BoardCoordinate> CoordinateOuterPosition = new List<BoardCoordinate>();
        public IList<GamePlayer> GamePlayers = new List<GamePlayer>();

        public int GamePlayerAmnt { get; set; } = 0;

        public string SessionName { get; set; } = "";

        public bool NewGame { get; set; }

        public Dice dice = new Dice();

        public string winner = "";

        public int saveOrSkip = 0;

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
            Console.OutputEncoding = Encoding.UTF8;


            if (NewGame == true)
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;

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
                for(int i = 0; i < GamePlayerAmnt; i++)
                {
                    GamePlayers[i].GlobalStartPos = SetColorStartPositon(GamePlayers[i].Color);
                }
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

                    MovePieceOnDiceResult(i,  options, selectedPiece, diceValue);

                    


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
                 
                    //Winner
                    var allPiecesFinished = GamePlayers[i].Pieces.All(p => p.PieceInGoal == true && p.CurrentPos == p.GoalPosIndex);
                    if (allPiecesFinished == true)
                    {
                        winner = GamePlayers[i].Name;                       
                    }

                }

                

                //Save Game or skip
                string[] saveOptions = { "Skip", "Save Game?" };
                saveOrSkip = CreateInteractable.OptionMenu(true, saveOptions, 40, gfxInfoPos);
                if (saveOrSkip == 1)
                {
                    Console.Clear();
                    DrawGFX.SetDrawPosition(0, 0);
                    Console.WriteLine("Saving data, please wait...");
                    DrawGFX.SetDrawPosition(0, 2);
                    SaveGame();                 
                    Console.Clear();
                }

            }

            SaveWinner();
            DisplayWinner();

        }

        /*===========================================================
        ////////////////////// GAMEPLAY /////////////////////////////
        ===========================================================*/       


        /*------------------DATABASE-RELATED-----------------------*/
        private void SaveGame()
        {
            for(int i = 0; i < GamePlayers.Count; i++)
            {
                for(int y= 0; y < GamePlayers[i].Pieces.Count; y++)
                {
                    Console.WriteLine($"{SessionName}, {GamePlayers[i].Name}, {GamePlayers[i].Pieces[y].PieceID}, {GamePlayers[i].Pieces[y].CurrentPos}");
                    gameData.UpdatePiecePosition(SessionName, GamePlayers[i].Name, GamePlayers[i].Pieces[y].PieceID, GamePlayers[i].Pieces[y].CurrentPos);
                }


            }

            return;
        }

        private void SaveWinner()
        {
            if(winner != "")
            {
                gameData.UpdateWinner(SessionName, true, winner);
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

        /*===========================================================
                                DICE-LOGIC
        ===========================================================*/

        private void MovePieceOnDiceResult(int playerIndex, IList<string> options, int selectedPiece, int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                    Console.WriteLine("Choose a piece to move");

                    options = CreatePieceBtnOptions(true, playerIndex);
                    if (options.Count() != 0)
                    {
                        selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                        string pieceName = options[selectedPiece];
                        int pieceID = int.Parse(pieceName.Last().ToString());

                        var pieceToMove = GetPieceByID(playerIndex, pieceID);
                        int pieceIndex = GetPieceIndex(playerIndex, pieceToMove);

                        MovePiece(playerIndex, pieceIndex, pieceToMove, diceValue);
                    }
                    break;

                case 6:
                    DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                    Console.WriteLine("You rolled 6. Please make a choice:");

                    List<string> moveOptions = new List<string>();
                    var piecesInNest = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos == p.LocalStartPos);
                    if (piecesInNest.Count() >= 2)
                    {
                        moveOptions.Add("Move 1 piece 6 steps?");
                        moveOptions.Add("Move 2 pieces 1 step?");
                    }
                    else
                        moveOptions.Add("Move 1 piece 6 steps?");

                    int selectMoveOption = CreateInteractable.OptionMenu(true, moveOptions, 0, gfxInteractablePos);

                    DrawGFX.ClearDrawContent(0, gfxInteractableInfoPos);

                    if (selectMoveOption == 0)
                    {
                        DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                        Console.WriteLine("Choose a piece to move");

                        options = CreatePieceBtnOptions(true, playerIndex);
                        if (options.Count() != 0)
                        {
                            selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                            string pieceName = options[selectedPiece];
                            int pieceID = int.Parse(pieceName.Last().ToString());

                            var pieceToMove = GetPieceByID(playerIndex, pieceID);
                            int pieceIndex = GetPieceIndex(playerIndex, pieceToMove);

                            MovePiece(playerIndex, pieceIndex, pieceToMove, 6);
                        }
                    }
                    else
                    {
                        for (int y = 1; y <= 2; y++)
                        {
                            DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                            Console.WriteLine($"Choose piece {playerIndex} to move");

                            IList<string> pieceOptions = CreatePieceButtonOptionsInNest(playerIndex);
                            if (pieceOptions.Count() != 0)
                            {
                                selectedPiece = (CreateInteractable.OptionMenu(true, pieceOptions, 0, gfxInteractablePos));
                                string pieceName = pieceOptions[selectedPiece];
                                int pieceID = int.Parse(pieceName.Last().ToString());

                                var pieceToMove = GetPieceByID(playerIndex, pieceID);
                                int pieceIndex = GetPieceIndex(playerIndex, pieceToMove);

                                MovePiece(playerIndex, pieceIndex, pieceToMove, 1);
                            }
                        }
                    }
                    break;
                default:
                    DrawGFX.SetDrawPosition(0, gfxInteractableInfoPos);
                    Console.WriteLine("Choose a piece to move");

                    options = CreatePieceBtnOptions(false, playerIndex);
                    if (options.Count() != 0)
                    {
                        selectedPiece = (CreateInteractable.OptionMenu(true, options, 0, gfxInteractablePos));
                        string pieceName = options[selectedPiece];
                        int pieceID = int.Parse(pieceName.Last().ToString());

                        var pieceToMove = GetPieceByID(playerIndex, pieceID);
                        int pieceIndex = GetPieceIndex(playerIndex, pieceToMove);

                        MovePiece(playerIndex, pieceIndex, pieceToMove, diceValue);
                    }
                    break;
            }
        }


        /*===========================================================
                                MOVEMENT
        ===========================================================*/


        /*------------------GAMEPLAYER & PIECE - RELATED------------------*/

        //Move the piece on the board
        private void MovePiece(int playerIndex, int pieceIndex, GamePiece pieceToMove, int diceValue)
        {
            int previousGlobalPosition = GetGlobalPosition(playerIndex, pieceIndex);
            int previousLocalPiecePosition = GetPreviousPieceLocalPosition(playerIndex, pieceIndex);
            int newLocalPiecePosition = GetNewLocalPiecePosition(playerIndex, pieceIndex, diceValue);
            int newGlobalPosition = GetGlobalPosition(playerIndex, pieceIndex);

            newGlobalPosition = CheckCollision(playerIndex, pieceIndex, previousGlobalPosition, newGlobalPosition);

            MoveLocalPiece(playerIndex, pieceIndex, previousLocalPiecePosition, newLocalPiecePosition);
            MoveGlobalPiece(playerIndex, pieceIndex, previousGlobalPosition, newGlobalPosition);

            //print positions to screen
            DrawGFX.SetDrawPosition(0, gfxSubInfoPos);
            Console.WriteLine($"Player {GamePlayers[playerIndex].GamePlayerID}, Piece {pieceToMove.PieceID} moved from position {previousGlobalPosition} to position {newGlobalPosition} in Game Board" +
                $" and to position {newLocalPiecePosition} in Player Board");
        }



        //Return a list of avaliable pieces to move, excluding pieces in goal position
        private IList<string> CreatePieceBtnOptions(bool displayInNest, int playerIndex)
        {
            IList<string> pieceOptions = new List<string>();

            if (displayInNest == true)
            {
                var piecesTrue = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos != p.GoalPosIndex);

                foreach (var id in piecesTrue)
                {
                    pieceOptions.Add($"Piece {id.PieceID}");
                }
            }
            else if (displayInNest == false)
            {
                var piecesFalse = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos != p.LocalStartPos && p.CurrentPos != p.GoalPosIndex);
                if (piecesFalse.Count() != 0)
                {
                    foreach (var id in piecesFalse)
                    {
                        pieceOptions.Add($"Piece {id.PieceID}");
                    }
                }
            }

            return pieceOptions;
        }

        //Return a list of only avaliable pieces to move
        private IList<string> CreatePieceButtonOptionsInNest(int playerIndex)
        {
            IList<string> pieceOptions = new List<string>();

                var pieces = GamePlayers[playerIndex].Pieces.Where(p => p.CurrentPos == p.LocalStartPos && p.CurrentPos != p.GoalPosIndex);

                foreach (var id in pieces)
                {
                    pieceOptions.Add($"Piece {id.PieceID}");
                }

            return pieceOptions;
        }

  

        //get index of a specific player by player Object
        private int GetPlayerIndex(GamePlayer gamePlayer)
        {
            return GamePlayers.IndexOf(gamePlayer);
        }

        //Get index of a specific piece
        private int GetPieceIndex(int playerIndex, GamePiece piece)
        {
            return GamePlayers[playerIndex].Pieces.IndexOf(GamePlayers[playerIndex].Pieces.Single(p => p.PieceID == piece.PieceID));
        }

        //Get a specific piece by ID
        private GamePiece GetPieceByID(int playerIndex, int pieceID)
        {
            return GamePlayers[playerIndex].Pieces.Where(s => s.PieceID == pieceID).FirstOrDefault();
        }


        /*------------------MOVEMENT-RELATED-----------------------*/

        //Call this before update to new local movement position (old position) AND after local movement (new position)
        private int GetGlobalPosition(int playerIndex, int pieceIndex)
        {
            GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;

            return GamePlayers[playerIndex].GlobalStartPos + (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos - 1);
        }

        //Call this before update to new local movement position
        private int GetPreviousPieceLocalPosition(int playerIndex, int pieceIndex)
        {
            return GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;
        }

        //Call this to get the new local position for piece to move to
        private int GetNewLocalPiecePosition(int playerIndex, int pieceIndex, int diceValue)
        {
            return GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos += diceValue;
        }

        //Call this method last, to move piece on local board
        private void MoveLocalPiece(int playerIndex, int pieceIndex, int previousPosition, int newLocalPosition)
        {
            int goalposition = GamePlayers[playerIndex].Pieces[pieceIndex].GoalPosIndex;

            if (previousPosition >= goalposition)
            {
                previousPosition = goalposition;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[previousPosition] = true;
            }
            else
            {
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[previousPosition] = false;
            }


            if (newLocalPosition >= goalposition)
            {
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos = goalposition;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[goalposition] = true;
                GamePlayers[playerIndex].Pieces[pieceIndex].PieceInGoal = true;
            }
            else if (newLocalPosition < goalposition)
            {
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[previousPosition] = false;
                GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos = newLocalPosition;
                GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[newLocalPosition] = true;
            }
        }

        //Call this method last, to move piece on global board
        private void MoveGlobalPiece(int playerIndex, int pieceIndex, int previousGlobalPosition, int newGlobalPosition)
        {
            int globalMaxPosition = CoordinateOuterPosition.Count - 1;

            if (newGlobalPosition >= globalMaxPosition)
                newGlobalPosition = (newGlobalPosition - globalMaxPosition);

            if (previousGlobalPosition >= globalMaxPosition)
                previousGlobalPosition = (previousGlobalPosition - globalMaxPosition);

            if (previousGlobalPosition < 0)
            {
                previousGlobalPosition = 0;
            }

            CoordinateOuterPosition[previousGlobalPosition].IsOccupied = false;
            CoordinateOuterPosition[previousGlobalPosition].OccupiedPlayerID = 0;
            CoordinateOuterPosition[previousGlobalPosition].OccupiedPlayerPieceID = 0;

            if (GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos > 40)
            {
                CoordinateOuterPosition[newGlobalPosition].IsOccupied = false;
                CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerID = 0;
            }
            else
            {
                CoordinateOuterPosition[newGlobalPosition].IsOccupied = true;
                CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerID = GamePlayers[playerIndex].GamePlayerID;
                CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerPieceID = GamePlayers[playerIndex].Pieces[pieceIndex].PieceID;
            }
        }

       
        /*------------------Collission-RELATED-----------------------*/

        private int CheckCollision(int playerIndex, int pieceIndex, int newLocalPosition, int newGlobalPosition)
        {
            DrawGFX.ClearDrawContent(0, 5, 8);

            int globalMaxPosition = CoordinateOuterPosition.Count - 1;

            if (newGlobalPosition >= globalMaxPosition)
                newGlobalPosition = (newGlobalPosition - globalMaxPosition);

            if (CoordinateOuterPosition[newGlobalPosition].IsOccupied == true)

            {               
                var currentPlayerID = GamePlayers[playerIndex].GamePlayerID;
                var currentPlayerColor = GamePlayers[playerIndex].Color;
                var currentPlayerName = GamePlayers[playerIndex].Name;
                var currentPlayerPieceID = GamePlayers[playerIndex].Pieces[pieceIndex].PieceID;

                var otherPlayerID = CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerID;
                var otherPlayerName = GamePlayers.Where(n => n.GamePlayerID == otherPlayerID).Select(n => n.Name).FirstOrDefault();
                var otherPlayerColor = GamePlayers.Where(n => n.GamePlayerID == otherPlayerID).Select(n => n.Color).FirstOrDefault();
                var otherPlayerPieceID = CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerPieceID;
                var otherPlayerObject = GamePlayers.Where(n => n.GamePlayerID == otherPlayerID).FirstOrDefault();

                int otherPlayerIndex = GetPlayerIndex(otherPlayerObject);
                var otherPlayerPiece = GetPieceByID(otherPlayerIndex, otherPlayerPieceID);
                int otherPlayerPieceIndex = GetPieceIndex(otherPlayerIndex, otherPlayerPiece);


                //måste ha metod som hanterar om piece tex gul piece är 0
                if (currentPlayerID != otherPlayerID)
                {                    
                    //print knockout-data to console
                    DrawGFX.ClearDrawContent(0, 5, 8);
                    DrawGFX.SetDrawPosition(50, 5);
                    Console.WriteLine("==KNOCK OUT!==");
                    DrawGFX.SetDrawPosition(50, 6);
                    Console.WriteLine($"{currentPlayerColor} Player {currentPlayerID}: {currentPlayerName}, {currentPlayerPieceID} knocked out:");
                    //DrawGFX.SetDrawPosition(50, 7);
                    //Console.WriteLine($"{otherPlayerColor} Player {otherPlayerID}, (gameplayer index {otherPlayerIndex}): {otherPlayerName}, piece {otherPlayerPieceID}, (piece index {otherPlayerIndex})");

                    //clear occupational boardindex
                    CoordinateOuterPosition[newGlobalPosition].IsOccupied = false;
                    CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerID = 0;
                    CoordinateOuterPosition[newGlobalPosition].OccupiedPlayerPieceID = 0;

                    //clear other player picepositions
                    int otherPlayerPieceLocalPos = GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentPos;

                    GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].LocalCoordinatePositions[otherPlayerPieceLocalPos] = false;
                    GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentPos = 0;
                    GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentGlobalPos = 0;
                    GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].LocalCoordinatePositions[otherPlayerPieceLocalPos] = true;

                    otherPlayerPieceLocalPos = GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentGlobalPos;

                    //print other player pice position after knockout
                    DrawGFX.SetDrawPosition(50, 8);
                    Console.WriteLine($"{otherPlayerColor} Player {otherPlayerID}: {otherPlayerName}, piece {otherPlayerPieceID} moved to position {otherPlayerPieceLocalPos}");

                }
                else if(currentPlayerID == otherPlayerID)
                {
                    if(newGlobalPosition != GamePlayers[otherPlayerIndex].GlobalStartPos)
                    {
                        //print move behind-data to console
                        DrawGFX.ClearDrawContent(0, 5, 8);
                        DrawGFX.SetDrawPosition(50, 5);
                        Console.WriteLine("==MOVE BEHIND==");
                        DrawGFX.SetDrawPosition(50, 6);
                        Console.WriteLine($"{currentPlayerColor} Player {currentPlayerID}: {currentPlayerName}, {currentPlayerPieceID} same position as self:");
                        DrawGFX.SetDrawPosition(50, 7);
                        Console.WriteLine($"{otherPlayerColor} Player {otherPlayerID}, (gameplayer index {otherPlayerIndex}): {otherPlayerName}, piece {otherPlayerPieceID}, (piece index {otherPlayerIndex})");


                        newGlobalPosition -=1;

                        int otherPlayerPieceLocalPos = GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentPos;
                        int currentplayerLocalPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos;

                        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[otherPlayerPieceLocalPos] = false;
                        GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--;
                        GamePlayers[playerIndex].Pieces[pieceIndex].CurrentGlobalPos--;

                        int currentLocalPos = GamePlayers[playerIndex].Pieces[pieceIndex].CurrentPos--;

                        GamePlayers[playerIndex].Pieces[pieceIndex].LocalCoordinatePositions[currentLocalPos] = true;
 
                        int otherPlayerPieceGlobalPos = GamePlayers[otherPlayerIndex].Pieces[otherPlayerPieceIndex].CurrentGlobalPos--;                                              

                        DrawGFX.SetDrawPosition(50, 8);
                        Console.WriteLine($"{otherPlayerColor} Player {otherPlayerID}: {otherPlayerName}, piece {otherPlayerPieceID} moved 1 step behind to global position {otherPlayerPieceGlobalPos} and local position {currentplayerLocalPos}");
                    }
                    
                }                   
            }

            return newGlobalPosition;
        }

       
        /*===========================================================
        //////////////////// INITIALIZE /////////////////////////////
        ===========================================================*/
        public void InitializeGame()
        {
            //sessionname
            SessionName = gs.GetSessionName();

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
