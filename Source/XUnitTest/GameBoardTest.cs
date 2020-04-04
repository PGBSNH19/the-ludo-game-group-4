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
        [InlineData("Red")]
        [InlineData("Blue")]
        [InlineData("Green")]
        [InlineData("Yellow")]
        public void SetColorStartPositon_InputColor_ReturnCorrectBoardPosition(string color)
        {
            //Arrange
            int expectedValue = 0;
            IGameSession gs = new GameSession();
            GameBoard gameBoard = new GameBoard(gs);

            if (color == "Red")
                expectedValue = 1;  //ev. 0 om man räknar från index 0
            else if (color == "Blue")
                expectedValue = 11;  //ev. 10 om man räknar från index 0
            else if (color == "Green")
                expectedValue = 21;  //ev. 20 om man räknar från index 0
            else if (color == "Yellow")
                expectedValue = 31;  //ev. 30 om man räknar från index 0

            //Act
            int actualValue = gameBoard.SetColorStartPositon(color);

            //Assert
            Assert.Equal(expectedValue, actualValue);
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
            Assert.Equal(gpExpected, gpp);

        }


    }
}
