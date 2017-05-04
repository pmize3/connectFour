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

    public class SimpleAI : Player
    {
        public SimpleAI(PlayerColor color) : base(color) { }

        public int ChooseColumn(ConnectFour game)
        {
            int index = 0;
            var board = game.Board;
            var otherPlayer = (this.Color == PlayerColor.Red) ? game.playerYellow : game.playerRed;
            foreach(var column in board.Cells)
            {
                foreach (var cell in column)
                {
                    if (cell.OccupyingPlayer == this) continue;

                    CellLocation direction;
                    var length = board.GetLongestConnectLength(cell, out direction);
                    if (length >= 3)
                    {
                        if (direction == CellLocation.Above || direction == CellLocation.Below)
                        {
                            if (board.Cells[index].All(c => c.OccupyingPlayer != null))
                            {
                                // we can't play in that row move on
                                continue;
                            }
                            else
                            {
                                return cell.X;
                            }
                        }

                        var firstCell = cell.Traverse(otherPlayer, direction);
                        var possiblePlayCell = firstCell.GetNeighbor(direction);
                        if (possiblePlayCell != null && possiblePlayCell.OccupyingPlayer == null && possiblePlayCell.GetNeighbor(CellLocation.Below)?.OccupyingPlayer != null)
                        {
                            return possiblePlayCell.X;
                        }

                        var reverseDirection = (CellLocation)(((int)direction + 4) % 8);
                        var lastCell = cell.Traverse(otherPlayer, reverseDirection);
                        possiblePlayCell = firstCell.GetNeighbor(reverseDirection);
                        if (possiblePlayCell != null && possiblePlayCell.OccupyingPlayer == null && possiblePlayCell.GetNeighbor(CellLocation.Below)?.OccupyingPlayer != null)
                        {
                            return possiblePlayCell.X;
                        }
                    }
                }
            }

            // otherwise just be random
            var rand = new Random(board.ColumnCount);
            index = rand.Next();
            while (board.Cells[index].All(c => c.OccupyingPlayer != null))
            {
                index = rand.Next();
            }

            return index;
        }
    }
}
