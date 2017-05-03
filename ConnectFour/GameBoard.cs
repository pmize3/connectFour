using System;

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
        private int victoryLength = 4;

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
            if (startingCell == null || startingCell.OccupyingPlayer == null)
            {
                return VictoryCondition.None;
            }
            
            // check surrounding cells and traverse
            for (int i = 0; i < Enum.GetValues(typeof(CellLocation)).Length; i++)
            {
                if (GetConnectLength(startingCell, (CellLocation)i) >= victoryLength - 1)
                {
                    return VictoryCondition.Victory;
                }
            }

            // check that we can still play
            foreach (var row in Cells)
            {
                foreach(var cell in row)
                {
                    if (cell.OccupyingPlayer == null)
                    {
                        return VictoryCondition.None;
                    }
                }
            }

            return VictoryCondition.Tie;
        }

        /// <summary>
        /// Iterates through neighboring cells to determine if there are any connect fours
        /// from the starting cell along the plane of the location specified.
        /// </summary>
        /// <param name="startingCell">The cell to start searching for connects.</param>
        /// <param name="location">The location/direction to look.</param>
        /// <returns>The length of the connections in that location plane.</returns>
        public int GetConnectLength(GridCell startingCell, CellLocation location)
        {
            var length = 0;
            var player = startingCell.OccupyingPlayer;
            var direction = location;
            var reverseDirection = (CellLocation)(((int)location + 4) % 8);

            // check first direction
            var firstMostCell = startingCell;
            while(firstMostCell.GetNeighbor(direction)?.OccupyingPlayer == player)
            {
                firstMostCell = firstMostCell.GetNeighbor(direction);
            }

            // check reverse direction
            var lastMostCell = startingCell;
            while(lastMostCell.GetNeighbor(reverseDirection)?.OccupyingPlayer == player)
            {
                lastMostCell = lastMostCell.GetNeighbor(reverseDirection);
            }

            // get the connection length
            length = firstMostCell.X - lastMostCell.X;

            // if we are looking vertically, we have to use Y diff instead of X
            if (direction == CellLocation.Above || direction == CellLocation.Below)
            {
                length = firstMostCell.Y - lastMostCell.Y;
            }


            return Math.Abs(length);
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
