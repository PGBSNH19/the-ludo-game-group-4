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
            Console.SetWindowSize(200, 50);
            Console.SetBufferSize(200, 50);

            //NYASTE GAMEDATAN
            //GameData d = new GameData();

            ////metod return unfinished Game from database. session name must match
            //foreach (var i in d.LoadGame("session2"))
            //{
            //    Console.WriteLine($"Session Name: {i.SessionName}\tPlayerName: {i.PlayerName}\tColor: {i.Color}\tPiece ID: {i.PieceID}\tPosition{i.Position}");
            //}

            ////setting winner of session   (sessionName, gameFinished, WinnerName)
            ////d.UpdateWinner("session1", true, "hamid");

            ////remove everything from all tables from database, we can remove this method,
            ////.RemoveEverything();

            ////Update position of piece (sessionName, playerName, pieceID(1-4), new position)
            ////d.UpdatePiecePosition("session2", "samir", 2, 22);

                      
            ////create session (name,GameFinished? WinnerName?. method must calls before inserteachPlayer()
             //d.InsertSessionData("session10", false, "");


            //code bellow create player with their piece and color. 
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

            //shows the recent added players and session
            //d.ShowData();

            //    //this method show the high score players
            //    //d.ShowHighScore();





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

            //GameBoard gb = new GameBoard(new GameSession());
            
            
        }
    }
}
