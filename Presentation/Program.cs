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
                Line();
                Console.WriteLine("| Select your operation:                  |");
                Line();
                Console.WriteLine("| 1 - Create order.                       |");
                Console.WriteLine("| 2 - Read order.                         |");
                Console.WriteLine("| 3 - Update order.                       |");
                Console.WriteLine("| 4 - Delete order.                       |");
                Console.WriteLine("| 5 - Exit.                               |");
                Line();
                Console.Write("\n> ");
                input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        ReadOrder();
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

            ImportantMessage("| Goodbye!                                |");
        }
        private static void ReadOrder()
        {
            var getInput = new GetInput();
            var getOutput = new GetOutput();

            int id = getInput.OrderID();
            var order = getOutput.GetOrderByID(id);
            //var orderDetails = getOutput.GetOrderDetailsByID(id);

            Console.Clear();
            Line();
            Console.WriteLine($"| Customer ID: {order.CustomerID}");
            Console.WriteLine($"| Employee ID: {order.EmployeeID}");
            Console.WriteLine($"| Order Date: {order.OrderDate}");
            Console.WriteLine($"| Required Date: {order.RequiredDate}");
            Console.WriteLine($"| Shipped Date: {order.ShippedDate}");
            Console.WriteLine($"| Ship Via: {order.ShipVia}");
            Console.WriteLine($"| Freight: {order.Freight}");
            Console.WriteLine($"| Ship Name: {order.ShipName}");
            Console.WriteLine($"| Ship Address: {order.ShipAddress}");
            Console.WriteLine($"| Ship City: {order.ShipCity}");
            Console.WriteLine($"| Ship Region: {order.ShipRegion}");
            Console.WriteLine($"| Ship Postal Code: {order.ShipPostalCode}");
            Console.WriteLine($"| Ship Country: {order.ShipCountry}");
            ImportantMessage("| Press any key to continue...            |");
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
            newOrderDTO.ShipVia = getInput.ShipVia("Please enter the ship via: ");
            newOrderDTO.Freight = getInput.positiveOrZeroDecimal("Please enter the freight: ");
            newOrderDTO.ShipName = getInput.ValidString("Please enter the ship name: ", 40);
            newOrderDTO.ShipAddress = getInput.ValidString("Please enter the ship address: ", 60);
            newOrderDTO.ShipCity = getInput.ValidString("Please enter the ship city: ", 15);
            newOrderDTO.ShipRegion = getInput.ValidString("Please enter the ship region: ", 15);
            newOrderDTO.ShipPostalCode = getInput.ValidString("Please enter the ship postal code: ", 10);
            newOrderDTO.ShipCountry = getInput.ValidString("Please enter the ship country: ", 15);
            
            var orderServices = new OrderServices();

            newOrderDTO.OrderID = orderServices.Create(newOrderDTO);

            getInput.OrderDetails(newOrderDTO.OrderID);

            var getOutput = new GetOutput();
            var amount = getOutput.CalculatePrice(newOrderDTO);
            ImportantMessage($"Orden Id {newOrderDTO.OrderID} con importe {amount} se ha creado correctamente");
            
        }
        private static void ImportantMessage(string text)
        {
            Line();
            Console.WriteLine(text);
            Line();
            Console.ReadLine();
        }
        private static void Line()
        {
            Console.WriteLine("+-----------------------------------------+");
        }
    }
}
