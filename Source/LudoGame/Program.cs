using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //just for testing ui
            IList<string> red1 = DrawUX.PlayerPieceBoard("red");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string s = "▲";
            Console.WriteLine(s);
            string b = "▲";
            Console.WriteLine(b);
            DrawUX.DrawAt(4, 0);
            Console.WriteLine(b);
            red1[0] = "(▲)";
            red1[43] = " ▲ ";
            red1[7] = " ▲ ";
            for (int i = 0; i <red1.Count; i++)
            {
                if (i == 0 || i >= 40 && i <= 44)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(red1[i]);
                }
                else if (i == 45)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(red1[i]+"\n\r");
                }
                else
                {
                    Console.ResetColor();
                    Console.Write(red1[i]);
                }               
            }

            //testing ui ends

            DataContext d = new DataContext();

            Menu.Display();

            Console.ReadKey();
        }
    }
}
