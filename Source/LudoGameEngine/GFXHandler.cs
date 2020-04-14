using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public enum BoardGFXItem
    {
        GameBoardGFX,
        PieceBoardGFX,
        GameBoardPiecesGFX
    }

    public enum PieceGFXItem
    {
        PieceOnBBoard,
        PieceInNest
    }

    public enum GameColors
    {
        Red,    //0
        Blue,   //1
        Green,  //2
        Yellow  //3
    }

    public class ConsolePosition
    {
        //ConsolePositions for positioning text and gfx-elements
        public static int InfoPos { get; } = 0;
        public static int SubInfoPos { get; } = 1;
        public static int StatusPos { get; } = 2;
        public static int ResultPos { get; } = 4;
        public static int InteractableInfoPos { get; } = 6;
        public static int InteractablePos { get; } = 8;
        public static int DividerPos { get; } = 10;

        public static int GameBoardTitlePos { get; } = 12;
        public static int GameBoardPiecePos { get; } = 14;
        public static int GameBoardPos { get; } = 15;
        public static int PlayerBoardTitlePos { get; } = 19;
        public static int PlayerInfoPos { get; } = 20;
        public static int PieceBoard1Pos { get; } = 22;
        public static int PieceBoard2Pos { get; } = 25;
        public static int PieceBoard3Pos { get; } = 28;
        public static int PieceBoard4Pos { get; } = 31;

    }

    public class DrawGFX
    {
        public static string GameBoardItem { get; } = "(x)";
        public static string PieceBoardItem { get; } = "(#)";
        public static string GameBoardPiecesItem { get; } = "   ";
        public static string PieceOnBoard { get; } = " ▲ ";
        public static string PieceInNest { get; } = "(▲)";       

        public static void SetDrawPosition(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }

        /// <summary>
        /// Clear a line
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public static void ClearDrawContent(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        /// <summary>
        /// Clear an area
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="startPosY"></param>
        /// <param name="endPosY"></param>
        public static void ClearDrawContent(int posX, int startPosY, int endPosY)
        {
            for(int i = startPosY; i <= endPosY; i++)
            {
                Console.SetCursorPosition(posX, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public static IList<string> CreateBoard(int boardLength, BoardGFXItem boardGFXItem)
        {
            string item = "";

            if (boardGFXItem == BoardGFXItem.GameBoardGFX)
                item = GameBoardItem;
            else if (boardGFXItem == BoardGFXItem.PieceBoardGFX)
                item = PieceBoardItem;
            else if (boardGFXItem == BoardGFXItem.GameBoardPiecesGFX)
                item = GameBoardPiecesItem;

            IList<string> tmp = new List<string>();
            for(int i = 0; i < boardLength; i++)
            {
                tmp.Add(item);
            }

            return tmp;
        }

        public static IList<string> RenderPieceBoard(string playerColor, IList<string> playerPieceBoard)
        {
            var boardColor = BrushColor(playerColor);

            for (int i = 0; i < playerPieceBoard.Count; i++)
            {
                if (i == 0 || i >= 41 && i <= 44)
                {
                    Console.ForegroundColor = boardColor;
                    Console.Write(playerPieceBoard[i]);
                }
                else if (i == 45)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(playerPieceBoard[i]);
                }
                else
                {
                    Console.ResetColor();
                    Console.Write(playerPieceBoard[i]);
                }
            }
            Console.ResetColor();

            return playerPieceBoard;
        }

        public static IList<string> RenderGameBoard(IList<string> commonGameBoard)
        {
            for (int i = 0; i < commonGameBoard.Count; i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(commonGameBoard[i]);
                }
                else if (i == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(commonGameBoard[i]);
                }
                else if (i == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(commonGameBoard[i]);
                }
                else if (i == 30)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(commonGameBoard[i]);
                }
                else if (i == commonGameBoard.Count - 1)
                {
                    Console.WriteLine(commonGameBoard[i]);
                }
                else
                {
                    Console.ResetColor();
                    Console.Write(commonGameBoard[i]);
                }
            }
            Console.ResetColor();

            return commonGameBoard;
        }

        public static IList<string> RenderGameBoardPieces(IList<string> commonGameBoardPieces, List<Tuple<int,int,string>> tmp)
        {
            foreach (var item in tmp)
            {
                commonGameBoardPieces[item.Item1] = PieceOnBoard;
                Console.ForegroundColor = BrushColor(item.Item3);
            }

            for (int i = 0; i < commonGameBoardPieces.Count; i++)
            {
                foreach(var item in tmp)
                {
                    if (item.Item1 == i)
                        Console.ForegroundColor = BrushColor(item.Item3);
                }

                if (i == commonGameBoardPieces.Count - 1)
                {
                    Console.WriteLine(commonGameBoardPieces[i]);
                    Console.ResetColor();
                }                                               
                else
                {
                    Console.Write(commonGameBoardPieces[i]);
                    Console.ResetColor();
                }
            }

            Console.ResetColor();

            return commonGameBoardPieces;
        }

        //Update gfx element for player when moved
        public static string UpdatePlayerPieceGFXByPosition(int position)
        {
            string pieceGFX = DrawGFX.PieceInNest;
            if (position != 0)
            {
                pieceGFX = DrawGFX.PieceOnBoard;
            }

            return pieceGFX;
        }

        public static ConsoleColor BrushColor(string color)
        {
            var brushColor = new ConsoleColor();

            if (color.ToLower() == "red")
            {
                brushColor = ConsoleColor.Red;
            }
            else if (color.ToLower() == "blue")
            {
                brushColor = ConsoleColor.Blue;
            }
            else if (color.ToLower() == "green")
            {
                brushColor = ConsoleColor.Green;
            }
            else if (color.ToLower() == "yellow")
            {
                brushColor = ConsoleColor.Yellow;
            }

            return brushColor;
        }
    }
}
