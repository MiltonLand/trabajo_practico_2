using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static void ShowMenu()
        {
            string input;
            do
            {
                Console.WriteLine("Select your operation: ");
                Console.WriteLine("1 - Create order.");
                Console.WriteLine("2 - Read order.");
                Console.WriteLine("3 - Update order.");
                Console.WriteLine("4 - Delete order.");
                Console.WriteLine("5 - Exit.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    default:
                        break;
                }

            } while (input != "5");

            Console.WriteLine("¡Hasta luego!");
            Console.ReadLine();
        }
    }
}
