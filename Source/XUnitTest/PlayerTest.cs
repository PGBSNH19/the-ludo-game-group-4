using System;
using Xunit;

namespace XUnitTest
{
    public class PlayerTest
    {
        [Fact]
        public int DiceRoll_ReturnIntValue()
        {
            Random number = new Random();

            int diceNumber = number.Next(1, 7);

            int value = 0;
            Type expected = value.GetType();

            Assert.IsType(expected, diceNumber);
            return diceNumber;
        }

        [Fact]
        public void DiceRoll_IntValueBetweenOneAndSix()
        {
            Random number = new Random();

            int diceNumber = number.Next(1, 7);

            Assert.True(diceNumber > 0 && diceNumber < 7);
        }
    }
}