using Xunit;
using System.Collections.Generic;
using LudoGameEngine;
using System.Linq;

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

        [Theory]
        [MemberData(nameof(TestDataForDecidePlayerStart))]
        public void DecidePlayerStart_SortOutStartingPlayer_StartingPlayer(int playerIdOne, int playerIdTwo, int playerIdThree, int playerIdFour, int diceNumberRollOne, int diceNumberRollTwo, int diceNumberRollThree, int diceNumberRollFour, int expectedPlayerId)
        {
            Dictionary<int, int> firstDiceRoll = new Dictionary<int, int>
            {
                { playerIdOne, diceNumberRollOne },
                { playerIdTwo, diceNumberRollTwo },
                { playerIdThree, diceNumberRollThree },
                { playerIdFour, diceNumberRollFour }
            };

            var startingPlayer = firstDiceRoll.OrderByDescending(x => x.Value).First();

            Assert.Equal(expectedPlayerId, startingPlayer.Key);
        }

        public static IEnumerable<object[]> TestDataForDecidePlayerStart =>
       new List<object[]>
       {
            new object[] { 101, 102, 103, 104, 4, 2, 1, 3, 101 },
            new object[] { 101, 102, 103, 104, 4, 2, 6, 1, 103 },
            new object[] { 101, 102, 103, 104, 4, 5, 1, 2, 102 },
            new object[] { 101, 102, 103, 104, 4, 5, 1, 6, 104 }
       };

        [Theory]
        [InlineData(false, 2, "Piece 1", "Piece 2", "Piece 3", "Piece 4")]
        [InlineData(true, 1, "Piece 1", "Piece 2", "Piece 3", "Piece 4")]
        [InlineData(false, 0, "Piece 1", "Piece 2", "Piece 3", "Piece 4")]
        [InlineData(true, 3, "Piece 1", "Piece 2", "Piece 3", "Piece 4")]
        public void CreatePieceButtonOptions_FindPiecePlacement_ReturnPieceIdentity(bool displayInNest, int index, string expectedOne, string expectedTwo, string expectedThree, string expectedFour)
        {
            IList<GamePlayer> gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer(1, "Laban", "Blue"),
                new GamePlayer(3, "John", "Green"),
                new GamePlayer(2, "Kalle", "Yellow"),
                new GamePlayer(4, "Johan", "Red")
            };

            IList<string> pieceOptions = new List<string>();

            var pieces = gamePlayers[index].Pieces.Where(p => p.CurrentPos != p.GoalPos).Select(p => p.PieceID);

            if (displayInNest != true)
                pieces = gamePlayers[index].Pieces.Where(p => p.CurrentPos != p.LocalStartPos || p.CurrentPos != p.GoalPos).Select(p => p.PieceID);

            foreach (var id in pieces)
            {
                pieceOptions.Add($"Piece {id}");
            }

            string actualOne = pieceOptions[0];
            string actualTwo = pieceOptions[1];
            string actualThree = pieceOptions[2];
            string actualFour = pieceOptions[3];

            Assert.Equal(expectedOne, actualOne);
            Assert.Equal(expectedTwo, actualTwo);
            Assert.Equal(expectedThree, actualThree);
            Assert.Equal(expectedFour, actualFour);
        }
    }
}
