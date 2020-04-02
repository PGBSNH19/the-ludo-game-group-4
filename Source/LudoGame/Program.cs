using LudoGameEngine;
using System;
using System.Collections.Generic;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {

            DataContext d = new DataContext();

            Menu.Display();

            Console.ReadKey();
        }
    }
}
