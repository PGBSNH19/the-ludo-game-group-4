using LudoGameEngine;
using System;
using System.Collections.Generic;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Player p1 = new Player { Name = "Samir", Piece = 4, Score = 5 };
            //Player p2 = new Player { Name = "Micael", Piece = 4, Score = 6 };
            //Player p3 = new Player { Name = "Pontus", Piece = 4, Score = 4 };
            //Player p4 = new Player { Name = "Ahmad", Piece = 4, Score = 3 };

            //List<Player> players = new List<Player>();
            //players.Add(p1);
            //players.Add(p2);
            //players.Add(p3);
            //players.Add(p4);

            // Detta är ett exampel på hur vi kan lösa uppgiften. vi skapar en klass med olika properties,
            //sedan skapar vi instans av klassen med olika personer. därefter lägger vi det i en lista av typen klass. 
            // varje index av listan kommer innehålla personer med olika poäng och pjässer

            //Console.WriteLine("Name \t" + players[0].Name + "\t Amount of Piece=\t" + players[0].Piece + "\tScore=\t" + players[0].Score);


            //foreach (var person in players)
            //{
            //    Console.WriteLine(person.Name);

            //}

            Menu.Display();

            Console.ReadKey();
        }
    }
}
