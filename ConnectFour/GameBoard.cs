using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class GameBoard
    {
        private GridCell[][] board;
        private int rowCount;
        private int columnCount;

        public GameBoard(int rows, int columns)
        {
            board = new GridCell[rows][];
            for (int i = 0; i < rows; i++)
            {
                var row = board[i];
                row = new GridCell[columns];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool DropInColumn(int column)
        {
            // check cells in the column


            // check victory condition
            return false;
        }
    }
}
