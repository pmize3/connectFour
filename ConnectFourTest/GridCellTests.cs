using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour;

namespace ConnectFourTest
{
    [TestClass]
    public class GridCellTests
    {
        private GridCell cell;

        [TestInitialize]
        public void TestInitialize()
        {
            cell = new GridCell(0, 0, null);
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

            var cell2 = new GridCell(1, 2, null);
            Assert.IsTrue(cell2.X == 1);
            Assert.IsTrue(cell2.Y == 2);
        }
    }
}
