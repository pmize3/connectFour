﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour;

namespace ConnectFourTest
{
    /// <summary>
    /// Summary description for ConnectFourTester
    /// </summary>
    [TestClass]
    public class ConnectFourTester
    {
        private ConnectFour.ConnectFour connect4;
        private int columnCount = 7;
        private int rowCount = 6;

        [TestInitialize]
        public void TestInitialize()
        {
            connect4 = new ConnectFour.ConnectFour(false);
        }

        [TestMethod]
        public void ConnectFour_ViewBoard_Test()
        {
            var board = new GameBoard(rowCount, columnCount);
            Assert.IsTrue(connect4.ViewBoard() == board.DrawBoard());
        }

        [TestMethod]
        public void ConnectFour_PlayTurn_Valid_Test()
        {
            var result = connect4.PlayTurn(1);
            Assert.IsTrue(result == TurnResult.Next);
        }

        [TestMethod]
        public void ConnectFour_PlayTurn_Invalid_Test()
        {
            var result = connect4.PlayTurn(-1);
            Assert.IsTrue(result == TurnResult.Invalid);

            result = connect4.PlayTurn(7);
            Assert.IsTrue(result == TurnResult.Invalid);
        }

        [TestMethod]
        public void ConnectFour_PlayTurn_Victory_Test()
        {
            TurnResult result;
            for (int i = 0; i < 6; i++)
            {
                result = connect4.PlayTurn(i % 2);
                Assert.IsTrue(result == TurnResult.Next);
            }

            result = connect4.PlayTurn(0);
            Assert.IsTrue(result == TurnResult.Victory);
        }

        [TestMethod]
        public void ConnectFour_PlayTurn_Tie_Test()
        {
            var result = TurnResult.Invalid;

            var column = 0;
            var row = 0;
            var evenPlayer = PlayerColor.Red;
            while (column < columnCount - 1)
            {
                var index = column;
                if (evenPlayer != connect4.CurrentPlayer.Color)
                {
                    index++;
                }

                result = connect4.PlayTurn(index);

                Assert.IsFalse(result == TurnResult.Victory);

                if (result != TurnResult.Invalid)
                {
                    // increment row, successful drop
                    row++;
                    if (row % 4 == 0)
                    {
                        evenPlayer = FlipPlayer(evenPlayer);
                    }
                }
                else
                {
                    // move to next column and reset
                    column += 2;
                    row = 0;
                    evenPlayer = FlipPlayer(evenPlayer);
                }
            }

            // play last column
            for (int i = 0; i < rowCount - 1; i++)
            {
                result = connect4.PlayTurn(columnCount - 1);
            }

            result = connect4.PlayTurn(columnCount - 1);

            Assert.IsTrue(result == TurnResult.Tie);
        }

        [TestMethod]
        public void ConnectFour_SimpleAI_Test()
        {
            connect4 = new ConnectFour.ConnectFour(true);
            TurnResult result;
            for (int i = 0; i < 5; i++)
            {
                result = connect4.PlayTurn(i % 2);
                Assert.IsTrue(result == TurnResult.Next);
            }

            var column = ((SimpleAI)connect4.CurrentPlayer).ChooseColumn(connect4);
            Assert.IsTrue(column == 0);
            result = connect4.PlayTurn(column);
            Assert.IsTrue(result == TurnResult.Next);

            for (int i = 0; i < 5; i++)
            {
                result = connect4.PlayTurn(i % 2 + 2);
                Assert.IsTrue(result == TurnResult.Next);
            }

            column = ((SimpleAI)connect4.CurrentPlayer).ChooseColumn(connect4);
            Assert.IsTrue(column == 2);
        }

        [TestMethod]
        public void ConnectFour_SimpleAI_Diagonal_Test()
        {
            connect4 = new ConnectFour.ConnectFour(true);
            TurnResult result;
            var columns = new int[] { 0, 1, 1, 2, 2, 3, 3, 0, 2, 3, 0 };
            for (int i = 0; i < columns.Length; i++)
            {
                result = connect4.PlayTurn(columns[i]);
                Assert.IsTrue(result == TurnResult.Next);
            }

            var column = ((SimpleAI)connect4.CurrentPlayer).ChooseColumn(connect4);
            Assert.IsTrue(column == 3);
            result = connect4.PlayTurn(column);
            Assert.IsTrue(result == TurnResult.Next);            
        }

        private PlayerColor FlipPlayer(PlayerColor player)
        {
            PlayerColor flippedPlayer = PlayerColor.Yellow;
            if (player != PlayerColor.Red)
            {
                flippedPlayer = PlayerColor.Red;
            }

            return flippedPlayer;
        }
    }
}
