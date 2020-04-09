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
        [InlineData("Red", 1)]
        [InlineData("Blue", 11)]
        [InlineData("Green", 21)]
        [InlineData("Yellow", 31)]
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
        public void SetPlayOrder_P1BlueP2Yellow_ExpectBlueYellow()
        {
            //Arrange
            IGameSession gs = new GameSession();
            GameBoard gb = new GameBoard(gs);
            IList<GamePlayer> gp = new List<GamePlayer>()
            {
                new GamePlayer(2, "kalle", "Yellow"),
                new GamePlayer(1, "Laban", "Blue")
            };

            IList<GamePlayer> gpExpected = new List<GamePlayer>()
            {
                new GamePlayer(1, "Laban", "Blue"),
                new GamePlayer(2, "kalle", "Yellow")
            };

            int playerID = 1;

            //Act
            gp = gb.SetPlayOrder(playerID, gp);

            //Assert
            Assert.Equal(gpExpected, gp);

        }


    }
}
