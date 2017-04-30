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
            
            Console.Write("Player Yellow, it's your turn. Choose a column (1-7)!");
            var column = 0;
            var columnText = Console.ReadLine();

            var isInt = int.TryParse(columnText, out column);
            if (!isInt || column > 7 || column < 1)
            {
                Console.Write("That is an invalid column. Please enter a number 1-7.");
            }
        }
    }
}
