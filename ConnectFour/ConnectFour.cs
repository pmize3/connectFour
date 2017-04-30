using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class ConnectFour
    {
        private GameBoard board;
        private Player playerYellow;
        private Player playerRed;
        private Player currentPlayer;
        private int turn;

        public ConnectFour()
        {
            turn = 0;
            board = new GameBoard(6, 7);
            currentPlayer = playerYellow = new Player(PlayerColor.Yellow);
            playerRed = new Player(PlayerColor.Red);
        }

        public Player CurrentPlayer { get { return currentPlayer; } }

        public Player NextTurn()
        {
            turn++;
            currentPlayer = (turn % 2 == 1) ? playerRed : playerYellow;
            return currentPlayer;
        }
    }
}
