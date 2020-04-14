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
         
            Menu.Display();
            Console.ReadKey();
            
        }
    }
}
