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
    }
}
