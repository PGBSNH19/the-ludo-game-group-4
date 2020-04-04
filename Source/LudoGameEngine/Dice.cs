using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class Dice
    {
        public int Roll()
        {
            Random rnd = new Random();
            return rnd.Next(0, 7);
        }
    }
}
