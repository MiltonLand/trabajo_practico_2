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
                MessageInsideBox("Select your operation:");
                Line();
                MessageInsideBox("1 - Create order.");
                MessageInsideBox("2 - Read order.");
                MessageInsideBox("3 - Update order.");
                MessageInsideBox("4 - Delete order.");
                MessageInsideBox("5 - Exit.");
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

            ImportantMessage("Goodbye!");
        }
        private static void ReadOrder()
        {
            var getInput = new GetInput();
            var getOutput = new GetOutput();

            int id = getInput.OrderID();
            var order = getOutput.GetOrderByID(id);
            var orderDetails = getOutput.GetOrderDetailsByID(id);

            Console.Clear();
            Line();
            MessageInsideBox($"Customer ID: {order.CustomerID}");
            MessageInsideBox($"Employee ID: {order.EmployeeID}");
            MessageInsideBox($"Order Date: {order.OrderDate}");
            MessageInsideBox($"Required Date: {order.RequiredDate}");
            MessageInsideBox($"Shipped Date: {order.ShippedDate}");
            MessageInsideBox($"Ship Via: {order.ShipVia}");
            MessageInsideBox($"Freight: {order.Freight}");
            MessageInsideBox($"Ship Name: {order.ShipName}");
            MessageInsideBox($"Ship Address: {order.ShipAddress}");
            MessageInsideBox($"Ship City: {order.ShipCity}");
            MessageInsideBox($"Ship Region: {order.ShipRegion}");
            MessageInsideBox($"Ship Postal Code: {order.ShipPostalCode}");
            MessageInsideBox($"Ship Country: {order.ShipCountry}");


            var productServices = new ProductServices();

            int count = 0;
            foreach (var od in orderDetails)
            {
                count++;
                Line();
                MessageInsideBox($"Bill Nro. {count}");
                MessageInsideBox($"Product: {productServices.GetProductNameById(od.ProductID)}");
                MessageInsideBox($"Unit price: {String.Format("{0:0.00}", od.UnitPrice)}");
                MessageInsideBox($"Quantity: {od.Quantity}");
                MessageInsideBox($"Discount: {od.Discount}");
            }

            ImportantMessage("Press any key to continue...");
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
            ReallyImportantMessage($"Orden Id {newOrderDTO.OrderID} con importe {String.Format("{0:0.00}", amount)} se ha creado correctamente.");
        }
        private static void ReallyImportantMessage(string text, int lineLength = 66)
        {
            Console.Clear();
            ImportantMessage(text);
        }
        private static void ImportantMessage(string text, int lineLength = 66)
        {
            Line();
            MessageInsideBox(text);
            Line();
            Console.ReadLine();
        }
        private static void MessageInsideBox(string text, int lineLength = 66)
        {
            int length = text.Count();
            lineLength -= 2;
            if (length < lineLength)
            {
                Console.Write("| " + text);
                for (int i = 0; i < lineLength - length - 1; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
        }
        private static void Line()
        {
            Console.WriteLine("+----------------------------------------------------------------+");
        }
    }
}
