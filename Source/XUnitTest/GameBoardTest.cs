using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LudoGameEngine;
using System.Linq;
using static LudoGameEngine.GameBoard;

namespace XUnitTest
{
    public class GameBoardTest
    {
        [Theory]
        [InlineData("Red")]
        [InlineData("Blue")]
        [InlineData("Green")]
        [InlineData("Yellow")]
        public void SetColorStartPositon_InputColor_ReturnCorrectBoardPosition(string color)
        {
            //Arrange
            int startPosition = 0;
            IGameSession gameSession = new GameSession();
            GameBoard gameBoard = new GameBoard(gameSession);

            if (color == "Red")
                startPosition = 1;  //ev. 0 om man räknar från index 0
            else if (color == "Blue")
                startPosition = 11;  //ev. 10 om man räknar från index 0
            else if (color == "Green")
                startPosition = 21;  //ev. 20 om man räknar från index 0
            else if (color == "Yellow")
                startPosition = 31;  //ev. 30 om man räknar från index 0

            //Act
            int actualValue = gameBoard.SetColorStartPositon(color);
            int expectedValue = startPosition;

            //Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SetPlayOrder_P1BlueP2YellowP3GreenP4Red_ExpectedOrderRedBlueGreenYellow()
        {
            //Arrange
            GameBoard gameBoard = new GameBoard(new GameSession());
            IList<GamePlayer> gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer(2, "Kalle", "Yellow"),
                new GamePlayer(1, "Laban", "Blue"),
                new GamePlayer(3, "Urban", "Green"),
                new GamePlayer(4, "Linus", "Red")
            };

            //Act
            var result = gameBoard.SetPlayOrder(gamePlayers[3].GamePlayerID, gamePlayers);

            //Assert
            Assert.Equal(4, result.Count);
            Assert.Equal("Red", result[0].Color);
            Assert.Equal("Yellow", result[1].Color);
            Assert.Equal("Blue", result[2].Color);
            Assert.Equal("Green", result[3].Color);
        }
    }
}
