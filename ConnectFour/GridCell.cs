using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class GridCell
    {
        private GameBoard board;
        public int X { get; private set; }
        public int Y { get; private set; }
        public Player OccupyingPlayer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridCell"/> class.
        /// </summary>
        /// <param name="xLocation">The x coordinate of the cell.</param>
        /// <param name="yLocation">The y coordinate of the cell.</param>
        /// <param name="board">The board the cell belongs to.</param>
        public GridCell(int xLocation, int yLocation, GameBoard board)
        {
            this.X = xLocation;
            this.Y = yLocation;
            this.board = board;
        }

        /// <summary>
        /// Gets the neighboring cell if one exists.
        /// </summary>
        /// <param name="location">The neighbor location.</param>
        /// <returns>The grid cell neighbor if it exists; otherwise null.</returns>
        private GridCell GetNeighbor(CellLocation location)
        {
            var x = this.X;
            var y = this.Y;

            switch (location)
            {
                case CellLocation.Left:
                    x--;
                    break;

                case CellLocation.Right:
                    x++;
                    break;

                case CellLocation.Above:
                    y++;
                    break;

                case CellLocation.Below:
                    y--;
                    break;

                case CellLocation.AboveLeft:
                    x--;
                    y++;
                    break;

                case CellLocation.AboveRight:
                    x++;
                    y++;
                    break;

                case CellLocation.BelowLeft:
                    x--;
                    y--;
                    break;

                case CellLocation.BelowRight:
                    x++;
                    y--;
                    break;
            }

            if (x < 0 || x >= this.board.ColumnCount || y < 0 || y >= this.board.RowCount)
            {
                return null;
            }

            return this.board.Cells[x][y];
        }

        /// <summary>
        /// Gets the cell to the left if one exists.
        /// </summary>
        public GridCell LeftCell
        {
            get { return GetNeighbor(CellLocation.Left); }
        }

        /// <summary>
        /// Gets the cell to the right if one exists.
        /// </summary>
        public GridCell RightCell
        {
            get { return GetNeighbor(CellLocation.Right); }
        }

        /// <summary>
        /// Gets the cell above if one exists.
        /// </summary>
        public GridCell AboveCell
        {
            get { return GetNeighbor(CellLocation.Above); }
        }

        /// <summary>
        /// Gets the cell below if one exists.
        /// </summary>
        public GridCell BelowCell
        {
            get { return GetNeighbor(CellLocation.Below); }
        }

        /// <summary>
        /// Gets the cell above and to the left if one exists.
        /// </summary>
        public GridCell AboveLeft
        {
            get { return GetNeighbor(CellLocation.AboveLeft); }
        }

        /// <summary>
        /// Gets the cell above and to the right if one exists.
        /// </summary>
        public GridCell AboveRight
        {
            get { return GetNeighbor(CellLocation.AboveRight); }
        }

        /// <summary>
        /// Gets the cell below and to the left if one exists.
        /// </summary>
        public GridCell BelowLeft
        {
            get { return GetNeighbor(CellLocation.BelowLeft); }
        }

        /// <summary>
        /// Gets the cell below and to the right if one exists.
        /// </summary>
        public GridCell BelowRight {
            get { return GetNeighbor(CellLocation.BelowRight); }
        }
        
        /// <summary>
        /// Attempts to put the player's marker in the cell.
        /// </summary>
        /// <param name="player">The player to occupy the cell.</param>
        /// <returns>
        /// True if the play succeeded and there wasn't another player 
        /// already there; otherwise false.
        /// </returns>
        public bool OccupyCell(Player player)
        {
            if (OccupyingPlayer == null)
            {
                OccupyingPlayer = player;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The neighboring cell locations.
        /// </summary>
        public enum CellLocation
        {
            Left,
            Right,
            Above,
            Below,
            AboveLeft,
            AboveRight,
            BelowLeft,
            BelowRight
        }
    }
}
