using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Player
    {
        public PlayerColor Color { get; private set; }
        public Player(PlayerColor color)
        {
            this.Color = color;
        }
    }

    public enum PlayerColor
    {
        Yellow,
        Red
    }

    public class SimpleAI : Player
    {
        public SimpleAI(PlayerColor color) : base(color) { }

        /// <summary>
        /// Finds a column to play for the AI. Tries to block winning moves.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>A valid column to place the marker.</returns>
        public int ChooseColumn(ConnectFour game)
        {
            int index = 0;
            var board = game.Board;
            var otherPlayer = (this.Color == PlayerColor.Red) ? game.playerYellow : game.playerRed;
            foreach(var column in board.Cells)
            {
                foreach (var cell in column)
                {
                    if (cell.OccupyingPlayer == null) continue;
                    if (cell.OccupyingPlayer == this) continue;

                    CellLocation direction;
                    var length = board.GetLongestConnectLength(cell, out direction);
                    if (length >= 3)
                    {
                        if (direction == CellLocation.Above || direction == CellLocation.Below)
                        {
                            var cellAbove = cell.Traverse(otherPlayer, CellLocation.Above).GetNeighbor(CellLocation.Above);
                            if (cellAbove == null || cellAbove.OccupyingPlayer != null)
                            {
                                // we can't play in that row move on
                                continue;
                            }
                            else
                            {
                                return cell.X;
                            }
                        }

                        var firstCell = cell.Traverse(otherPlayer, direction);
                        var possiblePlayCell = firstCell.GetNeighbor(direction);
                        if (CheckPlacement(possiblePlayCell))
                        {
                            return possiblePlayCell.X;
                        }

                        var reverseDirection = (CellLocation)(((int)direction + 4) % 8);
                        var lastCell = cell.Traverse(otherPlayer, reverseDirection);
                        possiblePlayCell = lastCell.GetNeighbor(reverseDirection);
                        if (CheckPlacement(possiblePlayCell))
                        {
                            return possiblePlayCell.X;
                        }
                    }
                }
            }

            // otherwise just be random
            var rand = new Random();
            index = rand.Next() % board.ColumnCount;
            while (board.Cells[index].All(c => c.OccupyingPlayer != null))
            {
                index = rand.Next() % board.ColumnCount;

            }

            return index;
        }

        /// <summary>
        /// Checks placement to see if it is a reasonable play.
        /// </summary>
        /// <remarks>
        /// Used for AI blocking player's winning move.
        /// </remarks>
        /// <param name="cell">The cell to play in.</param>
        /// <returns>True if reasonable; otherwise, false/</returns>
        private static bool CheckPlacement(GridCell cell)
        {
            if (cell != null && cell.OccupyingPlayer == null)
            {
                var possibleBelowCell = cell.GetNeighbor(CellLocation.Below);
                if (possibleBelowCell == null || possibleBelowCell.OccupyingPlayer != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
