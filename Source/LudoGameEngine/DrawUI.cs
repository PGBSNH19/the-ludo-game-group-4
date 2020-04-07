using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class DrawUX
    {
        private string piecePath = @"GFX/piece.txt";
        private string pieceBoardPath = @"GFX/pieceBoardItems.txt";
        private string gameBoardPath = @"GFX/gameBoardItem";

        private string pieceBoardItem = "(#)";
        public static string piece = "▲";
       
        //Färger på spelarplanen
        //if (i == 0 || i >= 40 && i <= 44)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.Write(red1[i]);
        //        }
        //        else if (i == 45)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            Console.Write(red1[i]+"\n\r");
        //        }
        //        else
        //        {
        //            Console.ResetColor();
        //            Console.Write(red1[i]);
        //        }               
        //}

        //static string Draw
        static void DrawAt(string gfx, int posX, int posY)
        {

        }
        public static void DrawAt(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }

        public static IList<string> PlayerPieceBoard(string color)
        {
            string s = ("(#)");

            IList<string> tmpList= new List<string>();

            for(int i = 0; i < 46; i++)
            {
                if (i == 0 || i >= 40 && i <= 44)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }                    
                else if(i == 45)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.ResetColor();

                tmpList.Add("(#)");
                
            }

            return tmpList;
        }

        static void BottomButtonsPosition()
        {

        }

        static void DrawBluePiece()
        {

        }
        
    }
}
