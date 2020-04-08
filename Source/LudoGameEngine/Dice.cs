using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class Dice
    {
        private Random random;

        public Dice()
        {
            this.random = new Random();
        }

        public Dice(Random rnd)
        {
            this.random = rnd;
        }

        public int Roll()
        {
            return random.Next(1, 7);
        }
    }
}
