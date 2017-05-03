using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    /// <summary>
    /// Represents the Connect Four game.
    /// </summary>
    public class ConnectFour
    {
        private GameBoard board;
        private Player playerYellow;
        private Player playerRed;
        private Player currentPlayer;
        private int turn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectFour"/> class.
        /// </summary>
        public ConnectFour()
        {
            turn = 0;
            board = new GameBoard(6, 7);
            currentPlayer = playerYellow = new Player(PlayerColor.Yellow);
            playerRed = new Player(PlayerColor.Red);
        }

        /// <summary>
        /// Gets the current player of this turn
        /// </summary>
        public Player CurrentPlayer { get { return currentPlayer; } }

        /// <summary>
        /// Plays the turn of the current player.
        /// </summary>
        /// <param name="column">The column to drop the current player's marker.</param>
        /// <returns>
        /// A TurnResult specifying if the play was invalid,
        /// if the result was victory for the current player,
        /// if the result is a tie, or if the play was valid 
        /// resulting the the next turn.
        /// </returns>
        public TurnResult PlayTurn(int column)
        {
            var cell = board.DropInColumn(currentPlayer, column);

            // placement was invalid
            if (cell == null)
            {
                return TurnResult.Invalid;
            }

            // check victory
            var condition = board.CheckVictory(cell);

            // if result was not next (victory or tie), return result
            if (condition != TurnResult.Next)
            {
                return condition;
            }

            // increment turn, switch players and return that it was a valid move
            turn++;
            currentPlayer = (turn % 2 == 1) ? playerRed : playerYellow;
            return TurnResult.Next;
        }

        /// <summary>
        /// Draws the board to the console in text.
        /// </summary>
        /// <returns>A string representation of the game board.</returns>
        public string ViewBoard()
        {
            return board.DrawBoard();
        }
    }
}
