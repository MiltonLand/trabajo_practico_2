using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

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
                Console.Clear();
                Console.WriteLine("Select your operation: ");
                Console.WriteLine("1 - Create order.");
                Console.WriteLine("2 - Read order.");
                Console.WriteLine("3 - Update order.");
                Console.WriteLine("4 - Delete order.");
                Console.WriteLine("5 - Exit.");
                input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        //ReadOrder();
                        break;
                    case "3":
                        //UpdateOrder();
                        break;
                    case "4":
                        //DeleteOrder();
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

        private static void CreateOrder()
        {
            var getInput = new GetInput();
            var newOrderDTO = new OrderDTO();

            newOrderDTO.CustomerID = getInput.CustomerID("Please enter the customer ID: "); ;
            newOrderDTO.EmployeeID = getInput.EmployeeID();
            newOrderDTO.OrderDate = getInput.Date("Please enter the order date: ");
            newOrderDTO.RequiredDate = getInput.Date("Please enter the required date: ");
            newOrderDTO.ShippedDate = getInput.Date("Please enter the shipped date: ");
            newOrderDTO.Freight = getInput.positiveOrZeroDecimal();
            newOrderDTO.ShipName = getInput.ValidString("Please enter the ship name: ", 40);
            newOrderDTO.ShipAddress = getInput.ValidString("Please enter the ship address: ", 60);
            newOrderDTO.ShipCity = getInput.ValidString("Please enter the ship city: ", 15);
            newOrderDTO.ShipRegion = getInput.ValidString("Please enter the ship region: ", 15);
            newOrderDTO.ShipPostalCode = getInput.ValidString("Please enter the ship postal code: ", 10);
            newOrderDTO.ShipCountry = getInput.ValidString("Please enter the ship country: ", 15);

            var orderServices = new OrderServices();

            Nullable<int> id = orderServices.AddNewOrder(newOrderDTO);

            if (id == null)
                Console.WriteLine("Oops, something went wrong!");
            else
                Console.WriteLine($"Orden Id {id} con importe {id} se ha creado correctamente");

            Console.ReadLine();
        }
    }
}
