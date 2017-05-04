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
            Console.Write($"Enter 0 to play humans. Enter 1 to play the CPU. ");
            var text = Console.ReadLine();
            int boolInt = 0;
            int.TryParse(text, out boolInt);
            var game = new ConnectFour(boolInt == 1);

            var condition = TurnResult.Invalid;
            do
            {
                condition = TurnResult.Invalid;

                var column = 0;
                var isInt = false;
                if (game.CurrentPlayer is SimpleAI)
                {
                    column = ((SimpleAI)game.CurrentPlayer).ChooseColumn(game) + 1;
                    isInt = true;
                    Console.WriteLine($"Player {game.CurrentPlayer.Color}, chooses column {column}");
                }
                else
                {

                    Console.Write($"Player {game.CurrentPlayer.Color}, it's your turn. Choose a column (1-7)");
                    var columnText = Console.ReadLine();
                    isInt = int.TryParse(columnText, out column);
                }

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
