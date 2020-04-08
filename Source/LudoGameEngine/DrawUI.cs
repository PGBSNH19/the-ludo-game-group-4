using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class DrawUI
    {
        private static string pieceBoardItem = "(#)";
        public static string piece = "▲";

        public static void SetDrawPosition(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }

        public static IList<string> PlayerPieceBoard(string color)
        {
            IList<string> tmpList= new List<string>();

            for(int i = 0; i < 46; i++)
            {
                tmpList.Add(pieceBoardItem);               
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

        //public static void moveuipiece(int id, int steps, IList<string> playerpieceboard)
        //{

        //}


        static void bottombuttonsposition()
        {

        }

        static void drawbluepiece()
        {

        }
        
    }
}
