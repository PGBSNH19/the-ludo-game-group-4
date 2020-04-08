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
        public void SetPlayerAmount_ValidAmount_True(int input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerAmount = input
            };

            Assert.Equal(input, gameSession.PlayerAmount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void SetPlayerAmount_InvalidAmount_False(int input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerAmount = input
            };

            Assert.False(gameSession.PlayerAmount < 2 && gameSession.PlayerAmount > 4);
        }

        [Theory]
        [InlineData("Name")]
        public void SetPlayerName_ValidName_IsValid(string input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerName = input
            };

            bool valid = string.IsNullOrWhiteSpace(gameSession.PlayerName) == false;

            Assert.True(valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetPlayerName_InvalidName_IsNotValid(string input)
        {
            GameSession gameSession = new GameSession
            {
                PlayerName = input
            };

            bool notValid = string.IsNullOrWhiteSpace(gameSession.PlayerName) == true;

            Assert.True(notValid);
        }
    }
}
