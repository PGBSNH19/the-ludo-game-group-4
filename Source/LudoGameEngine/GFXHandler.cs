using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public enum BoardGFXItem
    {
        GameBoardGFX,
        PieceBoardGFX
    }

    public enum PieceGFXItem
    {
        PieceOnBBoard,
        PieceInNest
    }

    public enum GameColors
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    public class DrawGFX
    {
        public static string GameBoardItem { get; } = "(x)";
        public static string PieceBoardItem { get; } = "(#)";
        public static string PieceOnBoard { get; } = " ▲ ";
        public static string PieceInNest { get; } = "(▲)";

        public static void SetDrawPosition(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }

        public static IList<string> CreateBoard(int boardLength, BoardGFXItem boardGFXItem)
        {
            string item = "";

            if (boardGFXItem == BoardGFXItem.GameBoardGFX)
                item = GameBoardItem;
            else if (boardGFXItem == BoardGFXItem.PieceBoardGFX)
                item = PieceBoardItem;

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
                if (i == 0 || i >= 40 && i <= 44)
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


        //private static ConsoleColor BrushColor(GameColors gameColor)
        //{
        //    var brushColor = new ConsoleColor();

        //    if (gameColor == GameColors.Red)
        //    {
        //        brushColor = ConsoleColor.Red;
        //    }
        //    else if (gameColor == GameColors.Blue)
        //    {
        //        brushColor = ConsoleColor.Blue;
        //    }
        //    else if (gameColor == GameColors.Green)
        //    {
        //        brushColor = ConsoleColor.Green;
        //    }
        //    else if (gameColor == GameColors.Yellow)
        //    {
        //        brushColor = ConsoleColor.Yellow;
        //    }

        //    return brushColor;
        //}


        static void bottombuttonsposition()
        {

        }        
    }
}
