using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LudoGameEngine;

namespace XUnitTest
{
    public class GameBoardTest
    {
        [Theory]
        [InlineData("Red", 0)]
        [InlineData("Blue", 10)]
        [InlineData("Green", 20)]
        [InlineData("Yellow", 30)]
        public void SetColorStartPositon_InputColor_ExpectedBoardPosition(string color, int expectedBoardPosition)
        {
            //Arrange
            GameBoard gameBoard = new GameBoard(new GameSession());

            //Act
            int actualValue = gameBoard.SetColorStartPositon(color);

            //Assert
            Assert.Equal(expectedBoardPosition, actualValue);
        }

        [Fact]
        public void SetPlayOrder_P1BlueP2YellowP3RedP4Green_ExpectedPlayingOrderBlueGreenYellowRed()
        {
            //Arrange
            GameBoard gameBoard = new GameBoard(new GameSession());
            IList<GamePlayer> gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer(1, "Laban", "Blue"),
                new GamePlayer(3, "John", "Green"),
                new GamePlayer(2, "Kalle", "Yellow"),
                new GamePlayer(4, "Johan", "Red")
            };

            //Act
            var result = gameBoard.SetPlayOrder(gamePlayers[0].GamePlayerID, gamePlayers);

            //Assert
            Assert.Equal("Blue", result[0].Color);
            Assert.Equal("Green", result[1].Color);
            Assert.Equal("Yellow", result[2].Color);
            Assert.Equal("Red", result[3].Color);
        }
    }
}
