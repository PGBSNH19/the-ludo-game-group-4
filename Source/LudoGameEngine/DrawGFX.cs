using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class DrawGFX
    {
        public static string defaultGameBoardItem = "(x)";
        public static string defaultPieceBoardItem = "(#)";
        public static string pieceOnBoard = " ▲ ";
        public static string pieceInNest = "(▲)";

        public static void SetDrawPosition(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }

        public static IList<string> PlayerPieceBoard(string color)
        {
            IList<string> tmpList= new List<string>();

            for(int i = 0; i < 46; i++)
            {
                tmpList.Add(defaultPieceBoardItem);               
            }

            tmpList = DrawPieceBoard(color, tmpList);

            return tmpList;
        }

        public static IList<string> DrawPieceBoard(string color, IList<string> playerPieceBoard)
        {
            var boardColor = BrushColor(color);

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

        public static IList<string> CommonGameBoard()
        {
            IList<string> tmpList = new List<string>();

            for (int i = 0; i < 40; i++)
            {
                tmpList.Add(defaultGameBoardItem);
            }

            tmpList = DrawGameBoard(tmpList);

            return tmpList;
        }
        public static IList<string> DrawGameBoard(IList<string> commonGameBoard)
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

       

        private static ConsoleColor BrushColor(string color)
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


        static void bottombuttonsposition()
        {

        }

        static void drawbluepiece()
        {

        }
        
    }
}
