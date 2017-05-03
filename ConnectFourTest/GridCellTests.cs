using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour;

namespace ConnectFourTest
{
    [TestClass]
    public class GridCellTests
    {
        private GridCell cell;
        private GameBoard board;
        private int columnCount = 7;
        private int rowCount = 6;

        [TestInitialize]
        public void TestInitialize()
        {
            board = new GameBoard(rowCount, columnCount);
            cell = board.Cells[0][0];
        }

        [TestMethod]
        public void GridCell_OccupyCell_Test()
        {
            var player = new Player(PlayerColor.Red);
            var result = cell.OccupyCell(player);
            Assert.IsTrue(result);
            Assert.IsNotNull(cell.OccupyingPlayer);
            Assert.IsTrue(cell.OccupyingPlayer == player);

            result = cell.OccupyCell(player);
            Assert.IsFalse(result);
            Assert.IsTrue(cell.OccupyingPlayer == player);

            var player2 = new Player(PlayerColor.Yellow);
            result = cell.OccupyCell(player2);
            Assert.IsFalse(result);
            Assert.IsTrue(cell.OccupyingPlayer == player);
        }

        [TestMethod]
        public void GridCell_Location_Test()
        {
            Assert.IsTrue(cell.X == 0);
            Assert.IsTrue(cell.Y == 0);

            var cell2 = board.Cells[1][2];
            Assert.IsTrue(cell2.X == 1);
            Assert.IsTrue(cell2.Y == 2);
        }

        [TestMethod]
        public void GridCell_GetNeighbor_Corner_Test()
        {
            var leftNeighbor = cell.GetNeighbor(CellLocation.Left);
            Assert.IsNull(leftNeighbor);

            var aboveLeftNeighbor = cell.GetNeighbor(CellLocation.AboveLeft);
            Assert.IsNull(aboveLeftNeighbor);

            var aboveNeighbor = cell.GetNeighbor(CellLocation.Above);
            Assert.IsNotNull(aboveNeighbor);
            Assert.IsTrue(aboveNeighbor.Y == cell.Y + 1);
            Assert.IsTrue(aboveNeighbor.X == cell.X);

            var aboveRightNeighbor = cell.GetNeighbor(CellLocation.AboveRight);
            Assert.IsNotNull(aboveRightNeighbor);
            Assert.IsTrue(aboveRightNeighbor.Y == cell.Y + 1);
            Assert.IsTrue(aboveRightNeighbor.X == cell.X + 1);

            var rightNeighbor = cell.GetNeighbor(CellLocation.Right);
            Assert.IsNotNull(rightNeighbor);
            Assert.IsTrue(rightNeighbor.X == cell.X + 1);
            Assert.IsTrue(rightNeighbor.Y == cell.Y);

            var belowRightNeighbor = cell.GetNeighbor(CellLocation.BelowRight);
            Assert.IsNull(belowRightNeighbor);

            var belowNeighbor = cell.GetNeighbor(CellLocation.Below);
            Assert.IsNull(belowNeighbor);

            var belowLeftNeighbor = cell.GetNeighbor(CellLocation.BelowLeft);
            Assert.IsNull(belowLeftNeighbor);
        }

        [TestMethod]
        public void GridCell_GetNeighbor_Center_Test()
        {
            var cell1 = board.Cells[3][3];
            var leftNeighbor = cell1.GetNeighbor(CellLocation.Left);
            Assert.IsNotNull(leftNeighbor);
            Assert.IsTrue(leftNeighbor.X == cell1.X - 1);
            Assert.IsTrue(leftNeighbor.Y == cell1.Y);

            var aboveLeftNeighbor = cell1.GetNeighbor(CellLocation.AboveLeft);
            Assert.IsNotNull(aboveLeftNeighbor);
            Assert.IsTrue(aboveLeftNeighbor.X == cell1.X - 1);
            Assert.IsTrue(aboveLeftNeighbor.Y == cell1.Y + 1);

            var aboveNeighbor = cell1.GetNeighbor(CellLocation.Above);
            Assert.IsNotNull(aboveNeighbor);
            Assert.IsTrue(aboveNeighbor.Y == cell1.Y + 1);
            Assert.IsTrue(aboveNeighbor.X == cell1.X);

            var aboveRightNeighbor = cell1.GetNeighbor(CellLocation.AboveRight);
            Assert.IsNotNull(aboveRightNeighbor);
            Assert.IsTrue(aboveRightNeighbor.Y == cell1.Y + 1);
            Assert.IsTrue(aboveRightNeighbor.X == cell1.X + 1);

            var rightNeighbor = cell1.GetNeighbor(CellLocation.Right);
            Assert.IsNotNull(rightNeighbor);
            Assert.IsTrue(rightNeighbor.X == cell1.X + 1);
            Assert.IsTrue(rightNeighbor.Y == cell1.Y);

            var belowRightNeighbor = cell1.GetNeighbor(CellLocation.BelowRight);
            Assert.IsNotNull(belowRightNeighbor);
            Assert.IsTrue(belowRightNeighbor.X == cell1.X + 1);
            Assert.IsTrue(belowRightNeighbor.Y == cell1.Y - 1);

            var belowNeighbor = cell1.GetNeighbor(CellLocation.Below);
            Assert.IsNotNull(belowNeighbor);
            Assert.IsTrue(belowNeighbor.X == cell1.X);
            Assert.IsTrue(belowNeighbor.Y == cell1.Y - 1);

            var belowLeftNeighbor = cell1.GetNeighbor(CellLocation.BelowLeft);
            Assert.IsNotNull(belowLeftNeighbor);
            Assert.IsTrue(belowLeftNeighbor.X == cell1.X - 1);
            Assert.IsTrue(belowLeftNeighbor.Y == cell1.Y - 1);
        }
    }
}
