using LudoGameEngine;
using System;
using Xunit;
using UnitTest.Fake;

namespace UnitTest
{
    public class DiceTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Roll_OutputInRange1to6(int randomNextStub)
        {
            var dice = new Dice(new RandomStub() {ExpectedNext = randomNextStub });

            int result = dice.Roll();

            Assert.InRange(result, 1, 6);
        }
    }
}