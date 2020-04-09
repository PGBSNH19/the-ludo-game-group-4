using LudoGameEngine;
using System;
using Xunit;

namespace XUnitTest
{
    public class GameSessionTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void NoLogic_Input_InRange1to5(int input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerAmount = input
            };

            Assert.InRange<int>(gameSession.PlayerAmount, 1, 5);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void NoLogic_Input_OutsideOfRange2to4(int input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerAmount = input
            };

            Assert.NotInRange(gameSession.PlayerAmount, 2, 4);
        }

        [Theory]
        [InlineData("Name")]
        public void IsPlayerNameValid_ValidInput_PlayerNameIsValid(string input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerName = input
            };

            bool result = gameSession.IsPlayerNameValid();

            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsPlayerNameValid_InvalidPlayerNameInput_PlayerNameIsInvalid(string input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerName = input
            };

            bool result = gameSession.IsPlayerNameValid();

            Assert.False(result);
        }
    }
}
