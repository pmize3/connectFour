using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    /// <summary>
    /// Represents the board of the Connect Four game.
    /// </summary>
    public class GameBoard
    {
        // board with columns of rows, to add vertically easily
        public GridCell[][] Cells { get; private set; }
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Intializes a new instance of the <see cref="GameBoard"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the board.</param>
        /// <param name="columns">The number of columns in the board.</param>
        public GameBoard(int rows, int columns)
        {
            this.RowCount = rows;
            this.ColumnCount = columns;
            this.Cells = new GridCell[columns][];
            for (int i = 0; i < columns; i++)
            {
                var row = Cells[i] = new GridCell[rows];
                for(int j = 0; j < rows; j++)
                {
                    row[j] = new GridCell(i, j, this);
                }
            }
        }

        /// <summary>
        /// Attempts to drop a player piece in a column.
        /// </summary>
        /// <remarks>
        /// The method will iterate thru the columns from bottom to top (0 - max)
        /// until it finds a cell it can occupy. If it can't find one, it returns false.
        /// </remarks>
        /// <param name="player">The player that drops the piece.</param>
        /// <param name="column">The column the piece is dropped.</param>
        /// <returns>True if the drop was successful; otherwise, false.</returns>
        public bool DropInColumn(Player player, int column)
        {
            // check params
            if (column >= this.ColumnCount || column < 0 || player == null)
            {
                return false;
            }

            var columnCells = Cells[column];
            // check cells in the column
            for (int i = 0; i < columnCells.Length; i++)
            {
                // if successful
                if (columnCells[i].OccupyCell(player))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the condition of victory.
        /// </summary>
        /// <param name="startingCell"></param>
        /// <returns>
        /// An enum of either None (if game continues),
        /// Tie (if game has ended in a tie), or Victory
        /// (if the player of the cell won).
        /// </returns>
        public VictoryCondition CheckVictory(GridCell startingCell)
        {
            VictoryCondition victory = VictoryCondition.None;

            if (startingCell == null) return victory;

            var startingX = startingCell.X;
            var startingY = startingCell.Y;
            var player = startingCell.OccupyingPlayer;

            // check surrounding cells and traverse

            return victory;
        }

        /// <summary>
        /// The possible victory conditions
        /// </summary>
        public enum VictoryCondition
        {
            None,
            Victory,
            Tie
        }
    }
}
