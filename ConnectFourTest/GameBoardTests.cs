using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour;

namespace ConnectFourTest
{
    [TestClass]
    public class GameBoardTests
    {
        private GameBoard board;
        private int columnCount = 7;
        private int rowCount = 6;

        [TestInitialize]
        public void TestInitialize()
        {
            board = new GameBoard(rowCount, columnCount);
        }

        [TestMethod]
        public void GameBoard_DropInColumn_Test()
        {
            var player = new Player(PlayerColor.Red);
            var result = board.DropInColumn(player, 1);
            Assert.IsTrue(result);

            // check cell
            var cell = board.Cells[1][0];
            Assert.IsNotNull(cell);
            Assert.IsNotNull(cell.OccupyingPlayer);
            Assert.AreEqual(cell.OccupyingPlayer, player);
        }

        [TestMethod]
        public void GameBoard_DropInColumn_ExceedRows_Test()
        {
            var player = new Player(PlayerColor.Red);

            bool result = false;
            for (int i = 0; i < rowCount; i++)
            {
                result = board.DropInColumn(player, 0);
                Assert.IsTrue(result);
            }

            // drop again and it should fail
            result = board.DropInColumn(player, 0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GameBoard_CellLocation_Test()
        {
            var column = 3;
            var row = 5;
            var cell = board.Cells[column][row];
            Assert.IsNotNull(cell);
            Assert.IsTrue(cell.X == column);
            Assert.IsTrue(cell.Y == row);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_SimpleHorizontal_Test()
        {
            var player = new Player(PlayerColor.Red);

            bool result = false;
            for (int i = 0; i < 4; i++)
            {
                result = board.DropInColumn(player, i);
                Assert.IsTrue(result);
            }

            var victory = board.CheckVictory(board.Cells[3][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.Victory);

            victory = board.CheckVictory(board.Cells[1][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.Victory);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_SimpleVertical_Test()
        {
            var player = new Player(PlayerColor.Red);

            bool result = false;
            for (int i = 0; i < 4; i++)
            {
                result = board.DropInColumn(player, 0);
                Assert.IsTrue(result);
            }

            var victory = board.CheckVictory(board.Cells[0][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.Victory);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_SimpleDiagonal_Test()
        {
            var player = new Player(PlayerColor.Red);
            var player2 = new Player(PlayerColor.Yellow);

            bool result = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    result = board.DropInColumn(player2, i);
                    Assert.IsTrue(result);
                }

                result = board.DropInColumn(player, i);
                Assert.IsTrue(result);
            }

            var victory = board.CheckVictory(board.Cells[0][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.Victory);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_NoOccupyingPlayer_Test()
        {
            var player = new Player(PlayerColor.Red);

            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                result = board.DropInColumn(player, i);
                Assert.IsTrue(result);
            }

            var victory = board.CheckVictory(board.Cells[3][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.None);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_NoVictory_Test()
        {
            var player = new Player(PlayerColor.Red);

            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                result = board.DropInColumn(player, i);
                Assert.IsTrue(result);
            }

            var victory = board.CheckVictory(board.Cells[0][0]);
            Assert.IsTrue(victory == GameBoard.VictoryCondition.None);
        }

        [TestMethod]
        public void GameBoard_CheckVictory_Tie_Test()
        {
            var player = new Player(PlayerColor.Red);
            var player2 = new Player(PlayerColor.Yellow);

            bool result = false;
            for (int i = 0; i < columnCount; i++)
            {
                var evenPlayer = (i % 2 == 1) ? player : player2;
                var oddPlayer = (i % 2 == 1) ? player2 : player;
                for (int j = 0; j < rowCount; j++)
                {
                    var tempPlayer = evenPlayer;
                    if (j/2 % 2 == 1)
                    {
                        tempPlayer = oddPlayer;
                    }

                    result = board.DropInColumn(tempPlayer, i);
                    Assert.IsTrue(result);

                    result = board.DropInColumn(tempPlayer, i);
                    Assert.IsTrue(result);

                    // extra increment since we added an extra time
                    j++;
                }
            }

            foreach (var row in board.Cells)
            {
                foreach (var cell in row)
                {
                    var victory = board.CheckVictory(cell);
                    Assert.IsTrue(victory == GameBoard.VictoryCondition.Tie);
                }
            }
        }
    }
}
