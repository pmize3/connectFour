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
        /// until it finds a cell it can occupy. If it can't find one, it returns null.
        /// </remarks>
        /// <param name="player">The player that drops the piece.</param>
        /// <param name="column">The column the piece is dropped.</param>
        /// <returns>The GridCell that was occupied if the drop was successful; otherwise, null.</returns>
        public GridCell DropInColumn(Player player, int column)
        {
            // check params
            if (column >= this.ColumnCount || column < 0 || player == null)
            {
                return null;
            }

            var columnCells = Cells[column];
            // check cells in the column
            for (int i = 0; i < columnCells.Length; i++)
            {
                // if successful
                if (columnCells[i].OccupyCell(player))
                {
                    return columnCells[i];
                }
            }

            return null;
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
        public TurnResult CheckVictory(GridCell startingCell)
        {
            if (startingCell == null || startingCell.OccupyingPlayer == null)
            {
                return TurnResult.Next;
            }
            
            // check surrounding cells
            if (GetLongestConnectLength(startingCell) >= victoryLength)
            {
                return TurnResult.Victory;
            }

            // check that we can still play
            foreach (var row in Cells)
            {
                foreach(var cell in row)
                {
                    if (cell.OccupyingPlayer == null)
                    {
                        return TurnResult.Next;
                    }
                }
            }

            return TurnResult.Tie;
        }

        /// <summary>
        /// Gets the longest connect length for the current cell.
        /// </summary>
        /// <param name="startingCell">The cell to start</param>
        /// <param name="direction">The initial direction to traverse.</param>
        /// <returns>The longest connect length.</returns>
        public int GetLongestConnectLength(GridCell startingCell, out CellLocation direction)
        {
            var maxDirections = Enum.GetValues(typeof(CellLocation)).Length;
            var maxLength = 0;
            direction = default(CellLocation);

            // check surrounding cells and traverse
            for (int i = 0; i < maxDirections; i++)
            {
                var tempLength = GetConnectLength(startingCell, (CellLocation)i);
                if (tempLength > maxLength)
                {
                    maxLength = tempLength;
                    direction = (CellLocation)i;
                }
            }

            return maxLength;
        }

        /// <summary>
        /// Gets the longest connect length for the current cell.
        /// </summary>
        /// <param name="startingCell">The cell to start</param>
        /// <returns>The longest connect length.</returns>
        public int GetLongestConnectLength(GridCell startingCell)
        {
            CellLocation direction;
            return GetLongestConnectLength(startingCell, out direction);
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
            GridCell firstMostCell = TraverseCells(startingCell, player, direction);

            // check reverse direction
            var lastMostCell = TraverseCells(startingCell, player, reverseDirection);

            // get the connection length
            length = firstMostCell.X - lastMostCell.X;

            // if we are looking vertically, we have to use Y diff instead of X
            if (direction == CellLocation.Above || direction == CellLocation.Below)
            {
                length = firstMostCell.Y - lastMostCell.Y;
            }


            return Math.Abs(length) + 1;
        }

        /// <summary>
        /// Traverses the cells in the direction specified. Will stop when the next cell is not occupied by the player.
        /// </summary>
        /// <param name="startingCell">The cell to start with.</param>
        /// <param name="player">The player.</param>
        /// <param name="direction">The direction to traverse.</param>
        /// <returns>The furthest player occupied cell that connects to the starting cell.</returns>
        public static GridCell TraverseCells(GridCell startingCell, Player player, CellLocation direction)
        {
            var firstMostCell = startingCell;
            while (firstMostCell.GetNeighbor(direction)?.OccupyingPlayer == player)
            {
                firstMostCell = firstMostCell.GetNeighbor(direction);
            }

            return firstMostCell;
        }

        /// <summary>
        /// Gets a string representation of the board.
        /// </summary>
        /// <returns>A string representation of the board.</returns>
        public string DrawBoard()
        {
            string str = string.Empty;
            for(int y = RowCount - 1; y >= 0; y--)
            {
                for (int x = 0; x < ColumnCount; x++)
                {
                    var cellStr = "O";
                    var cell = Cells[x][y];
                    switch (cell.OccupyingPlayer?.Color)
                    {
                        case PlayerColor.Red:
                            cellStr = "R";
                            break;

                        case PlayerColor.Yellow:
                            cellStr = "Y";
                            break;
                    }

                    str += "\t" + cellStr;
                }
                str += "\n";
            }

            return str;
        }
    }

    /// <summary>
    /// The possible end of turn conditions
    /// </summary>
    public enum TurnResult
    {
        Next,
        Victory,
        Tie,
        Invalid
    }
}
