using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class GridCell
    {
        private int x;
        private int y;
        private Player occupyingPlayer;

        public GridCell(int xLocation, int yLocation)
        {
            this.x = xLocation;
            this.y = yLocation;
        }

        public bool OccupyCell(Player player)
        {
            if (occupyingPlayer == null)
            {
                occupyingPlayer = player;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
