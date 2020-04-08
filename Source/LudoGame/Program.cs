using LudoGameEngine;
using LudoGameEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ////just for testing ui
            //IList<string> red1 = DrawUI.PlayerPieceBoard("red");
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //string s = "▲";
            //Console.WriteLine(s);
            //string b = "▲";
            //Console.WriteLine(b);
            //DrawUI.DrawAt(4, 0);
            //Console.WriteLine(b);
            //red1[0] = "(▲)";
            //red1[43] = " ▲ ";
            //red1[7] = " ▲ ";
            //for (int i = 0; i <red1.Count; i++)
            //{
            //    if (i == 0 || i >= 40 && i <= 44)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.Write(red1[i]);
            //    }
            //    else if (i == 45)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Cyan;
            //        Console.Write(red1[i]+"\n\r");
            //    }
            //    else
            //    {
            //        Console.ResetColor();
            //        Console.Write(red1[i]);
            //    }               
            //}

            ////testing ui ends


            //GameData d = new GameData();

            ////metod return unfinished Game from database
            //foreach (var i in d.LoadGame())
            //{
            //    //Console.WriteLine($"Session Name: {i.SessionName}\tPlayerName: {i.PlayerName}\tColor: {i.Color}\tPiece ID: {i.PieceID}\tPosition{i.Position}");
            //}

            ////setting winner of session   (sessionName, gameFinished, WinnerName)
            ////d.updateWinner("session2", false, "Samir");

            ////remove everything from all tables from database, we can remove this method,
            ////d.RemoveEverything();

            ////Update position of piece (sessionName, playerName, pieceID(1-4), new position)
            ////d.UpdatePiecePosition("session2", "samir", 2, 22);


            ////create session (name,GameFinished? WinnerName?. method must calls before inserteachPlayer()
            //d.InsertSessionData("session2", true, "micael");


            ////code bellow create player with their piece and color. 
            //Console.WriteLine("Enter amount of player");
            //int amountPlayer = int.Parse(Console.ReadLine());
            //for (int i = 1; i <= amountPlayer; i++)
            //{
            //    Console.WriteLine($"Enter player {i} name");
            //    string name = Console.ReadLine();
            //    Console.WriteLine($"Enter player {i} color");
            //    string color = Console.ReadLine();
            //    //amount of player, name, color
            //    d.InsertEachPlayerData(i, name, color);
            //}


            ////shows the recent added players and session
            //d.ShowData();

            ////this method show the high score players
            ////d.ShowHighScore();

            Menu.Display();
            Console.ReadKey();  
        }
    }
}
        