using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Player
    {
        private PlayerColor color;
        public Player(PlayerColor color)
        {
            this.color = color;
        }

        public bool PlaceDisc(int column)
        {
            return false;
        }
    }

    public enum PlayerColor
    {
        Yellow,
        Red
    }
}
