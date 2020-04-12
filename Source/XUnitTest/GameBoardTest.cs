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
            GameBoard gameBoard = new GameBoard(new GameSession(),true);

            //Act
            int actualValue = gameBoard.SetColorStartPositon(color);

            //Assert
            Assert.Equal(expectedBoardPosition, actualValue);
        }

        [Fact]
        public void SetPlayOrder_P1BlueP2YellowP3RedP4Green_ExpectedPlayingOrderBlueGreenYellowRed()
        {
            //Arrange
            GameBoard gameBoard = new GameBoard(new GameSession(),true);
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
        [MemberData(nameof(TestDataForCreatePieceButtonOptions))]
        public void CreatePieceButtonOptions_FindPiecePlacement_ReturnPiecesNotInGoal(bool displayInNest,int index, int expected)
        {
            IList<GamePlayer> gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer(1, "Laban", "Blue"),
                new GamePlayer(3, "John", "Green"),
                new GamePlayer(2, "Kalle", "Yellow"),
                new GamePlayer(4, "Johan", "Red")
            };

            gamePlayers[0].Pieces[0].CurrentPos = 12;
            gamePlayers[0].Pieces[1].CurrentPos = 1;
            gamePlayers[0].Pieces[2].CurrentPos = 0;
            gamePlayers[0].Pieces[3].CurrentPos = 24;
            
            gamePlayers[1].Pieces[0].CurrentPos = 28;
            gamePlayers[1].Pieces[1].CurrentPos = 45;
            gamePlayers[1].Pieces[2].CurrentPos = 5;
            gamePlayers[1].Pieces[3].CurrentPos = 31;
            
            gamePlayers[2].Pieces[0].CurrentPos = 0;
            gamePlayers[2].Pieces[1].CurrentPos = 30;
            gamePlayers[2].Pieces[2].CurrentPos = 21;
            gamePlayers[2].Pieces[3].CurrentPos = 45;

            gamePlayers[3].Pieces[0].CurrentPos = 29;
            gamePlayers[3].Pieces[1].CurrentPos = 16;
            gamePlayers[3].Pieces[2].CurrentPos = 45;
            gamePlayers[3].Pieces[3].CurrentPos = 0;

            IList<string> pieceOptions = new List<string>();

            var pieces = gamePlayers[index].Pieces.Where(p => p.CurrentPos != p.GoalPosIndex).Select(p => p.PieceID);

            if (displayInNest != true)
                pieces = gamePlayers[index].Pieces.Where(p => p.CurrentPos != p.LocalStartPos || p.CurrentPos != p.GoalPosIndex).Select(p => p.PieceID);

            foreach (var id in pieces)
            {
                pieceOptions.Add($"Piece {id}");
            }

            var actual = pieceOptions.Count;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> TestDataForCreatePieceButtonOptions =>
      new List<object[]>
      {
            new object[] { false, 0, 4 },
            new object[] { true, 0, 4 },
            new object[] { true, 1, 3 },
            new object[] { true, 2, 3 },
            new object[] { true, 2, 3 },
            new object[] { true, 3, 3 },
            new object[] { true, 3, 3 },
      };

        //UpdateLocalPiecePosition(int playerIndex, GamePiece piece, int stepsToMove)



        
        //[Fact]       
        //public void UpdateLocalPiecePosition_TryMovePieceOutOfIndex_PieceSetToGoalAndNotOutOfIndex()
        //{
        //    IList<GamePlayer> gamePlayers = new List<GamePlayer>()
        //    {
        //        new GamePlayer(1, "Laban", "Blue"),
        //        new GamePlayer(3, "John", "Green"),
        //        new GamePlayer(2, "Kalle", "Yellow"),
        //        new GamePlayer(4, "Johan", "Red")
        //    };

        //    gamePlayers[0].Pieces[0].CurrentPos = 40;
        //    gamePlayers[0].Pieces[1].CurrentPos = 41;
        //    gamePlayers[0].Pieces[2].CurrentPos = 44;
        //    gamePlayers[0].Pieces[3].CurrentPos = 43;

        //    var gp1 = gamePlayers[0].Pieces[0];
        //    var gp2 = gamePlayers[0].Pieces[1];
        //    var gp3 = gamePlayers[0].Pieces[2];
        //    var gp4 = gamePlayers[0].Pieces[3];

        //    //IGameSession gs = new GameSession();

        //    int expected = 45;

        //    GameBoard gb = new GameBoard(new GameSession(),true);
        //    gb.GamePlayers = gamePlayers;


        //    //int actual = gb.UpdateLocalPiecePosition(0, gp4, 5);

        //    Assert.Equal(expected, actual);

        //}


        [Fact]
        public void GetGlobalPiecePosition_TryMovePieceOutOfIndex_LoopBackBeginCountFromIndex0()
        {
            IList<GamePlayer> gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer(1, "Laban", "Red"),
                new GamePlayer(3, "John", "Blue"),
                new GamePlayer(2, "Kalle", "Green"),
                new GamePlayer(4, "Johan", "Yellow")
            };

            gamePlayers[0].GlobalStartPos = 0;
            gamePlayers[1].GlobalStartPos = 10;
            gamePlayers[2].GlobalStartPos = 20;
            gamePlayers[3].GlobalStartPos = 30;

            var gp1 = gamePlayers[0].Pieces[0];
            var gp2 = gamePlayers[0].Pieces[1];
            var gp3 = gamePlayers[0].Pieces[2];
            var gp4 = gamePlayers[0].Pieces[3];

            int expected = 22;

            GameBoard gb = new GameBoard(new GameSession(), true);
            gb.GamePlayers = gamePlayers;

            for (int i = 0; i < 40; i++)
            {
                gb.CoordinateOuterPosition.Add(new BoardCoordinate());
            }



            int actual = gb.GetNewGlobalPiecePosition(2, 43);

            Assert.Equal(expected, actual);

        }



        
    }
}
