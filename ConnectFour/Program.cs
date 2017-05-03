using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new ConnectFour();

            var condition = TurnResult.Invalid;
            do
            {
                condition = TurnResult.Invalid;
                Console.Write($"Player {game.CurrentPlayer.Color}, it's your turn. Choose a column (1-7)");
                var column = 0;
                var columnText = Console.ReadLine();

                var isInt = int.TryParse(columnText, out column);

                if (isInt)
                {
                    condition = game.PlayTurn(column - 1);
                }

                if (condition == TurnResult.Invalid)
                {
                    Console.WriteLine("That is an invalid column.");
                }
                else
                {
                    Console.WriteLine(game.ViewBoard());
                }
            } while (condition != TurnResult.Tie && condition != TurnResult.Victory);

            if (condition == TurnResult.Tie)
            {
                Console.WriteLine("The game has ended in a tie!");
            }
            else
            {
                Console.WriteLine($"Player {game.CurrentPlayer.Color} is the victor!");
            }

            Console.ReadLine();
        }
    }
}
