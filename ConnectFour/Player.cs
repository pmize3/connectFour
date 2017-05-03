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
