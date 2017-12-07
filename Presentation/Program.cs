﻿using System;
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
                input = StandBy();
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
                        UpdateOrder();
                        break;
                    case "4":
                        //DeleteOrder();
                        break;
                    case "5":
                        break;
                    default:
                        ReallyImportantMessage("Invalid operation. ");
                        break;
                }

            } while (input != "5");

            ImportantMessage("Goodbye!");
        }

        private static void UpdateOrder()
        {
            var getInput = new GetInput();
            var getOutput = new GetOutput();
            
            int id = getInput.OrderID();
            var orderDto = getOutput.GetOrderByID(id);

            string input;

            do
            {
                Console.Clear();
                Line();
                MessageInsideBox("Select the field you want to update: ");
                Line();
                MessageInsideBox($"1 - Customer ID: {orderDto.CustomerID}");
                MessageInsideBox($"2 - Employee ID: {orderDto.EmployeeID}");
                MessageInsideBox($"3 - Order Date: {orderDto.OrderDate}");
                MessageInsideBox($"4 - Required Date: {orderDto.RequiredDate}");
                MessageInsideBox($"5 - Shipped Date: {orderDto.ShippedDate}");
                MessageInsideBox($"6 - Ship Via: {orderDto.ShipVia}");
                MessageInsideBox($"7 - Freight: {String.Format("{0:0.00}", orderDto.Freight)}");
                MessageInsideBox($"8 - Ship Name: {orderDto.ShipName}");
                MessageInsideBox($"9 - Ship Address: {orderDto.ShipAddress}");
                MessageInsideBox($"10 - Ship City: {orderDto.ShipCity}");
                MessageInsideBox($"11 - Ship Region: {orderDto.ShipRegion}");
                MessageInsideBox($"12 - Ship Postal Code: {orderDto.ShipPostalCode}");
                MessageInsideBox($"13 - Ship Country: {orderDto.ShipCountry}");
                MessageInsideBox("14 - Edit all fields.");
                MessageInsideBox("15 - Save changes.");
                MessageInsideBox("16 - Go back.");
                Line();
                input = StandBy();

                switch (input)
                {
                    case "1":
                        orderDto.CustomerID = getInput.CustomerID("Enter the new customed ID: ");
                        break;
                    case "2":
                        orderDto.EmployeeID = getInput.EmployeeID();
                        break;
                    case "3":
                        orderDto.OrderDate = getInput.Date("Enter the new order date: ");
                        break;
                    case "4":
                        orderDto.RequiredDate = getInput.Date("Enter the new required date: ");
                        break;
                    case "5":
                        orderDto.ShippedDate = getInput.Date("Enter the new shipped date: ");
                        break;
                    case "6":
                        orderDto.ShipVia = getInput.ShipVia("Enter the new ship via: ");
                        break;
                    case "7":
                        orderDto.Freight = getInput.PositiveOrZeroDecimal("Enter the new freight: ");
                        break;
                    case "8":
                        orderDto.ShipName = getInput.ValidString("Enter the new ship name: ", 40);
                        break;
                    case "9":
                        orderDto.ShipAddress = getInput.ValidString("Enter the new ship address: ", 60);
                        break;
                    case "10":
                        orderDto.ShipCity = getInput.ValidString("Enter the new ship city: ", 15);
                        break;
                    case "11":
                        orderDto.ShipRegion = getInput.ValidString("Enter the new ship region: ", 15);
                        break;
                    case "12":
                        orderDto.ShipPostalCode = getInput.ValidString("Enter the new ship postal code: ", 10);
                        break;
                    case "13":
                        orderDto.ShipCountry = getInput.ValidString("Enter the new ship country: ", 15);
                        break;
                    case "14":
                        orderDto.CustomerID = getInput.CustomerID("Enter the new customed ID: ");
                        orderDto.EmployeeID = getInput.EmployeeID();
                        orderDto.OrderDate = getInput.Date("Enter the new order date: ");
                        orderDto.RequiredDate = getInput.Date("Enter the new required date: ");
                        orderDto.ShippedDate = getInput.Date("Enter the new shipped date: ");
                        orderDto.ShipVia = getInput.ShipVia("Enter the new ship via: ");
                        orderDto.Freight = getInput.PositiveOrZeroDecimal("Enter the new freight: ");
                        orderDto.ShipName = getInput.ValidString("Enter the new ship name: ", 40);
                        orderDto.ShipAddress = getInput.ValidString("Enter the new ship address: ", 60);
                        orderDto.ShipCity = getInput.ValidString("Enter the new ship city: ", 15);
                        orderDto.ShipRegion = getInput.ValidString("Enter the new ship region: ", 15);
                        orderDto.ShipPostalCode = getInput.ValidString("Enter the new ship postal code: ", 10);
                        orderDto.ShipCountry = getInput.ValidString("Enter the new ship country: ", 15);
                        break;
                    case "15":
                        var orderServices = new OrderServices();
                        orderServices.Update(orderDto);
                        ReallyImportantMessage("Changes saved");
                        break;
                    case "16":
                        break;
                    default:
                        ReallyImportantMessage("Invalid field. ");
                        break;
                }
            } while ((input != "15") && (input != "16"));
        }
        private static void ReadOrder()
        {
            var getInput = new GetInput();
            var getOutput = new GetOutput();

            int id = getInput.OrderID();
            var order = getOutput.GetOrderByID(id);
            var orderDetails = getOutput.GetOrderDetailsByID(id);

            if ((order == null) || (orderDetails == null))
                ReallyImportantMessage("Error.");

            Console.Clear();
            Line();
            MessageInsideBox($"Order Nro. {id}");
            Line();
            MessageInsideBox($"Customer ID: {order.CustomerID}");
            MessageInsideBox($"Employee ID: {order.EmployeeID}");
            MessageInsideBox($"Order Date: {order.OrderDate}");
            MessageInsideBox($"Required Date: {order.RequiredDate}");
            MessageInsideBox($"Shipped Date: {order.ShippedDate}");
            MessageInsideBox($"Ship Via: {order.ShipVia}");
            MessageInsideBox($"Freight: {String.Format("{0:0.00}", order.Freight)}");
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
                Line();
                MessageInsideBox($"Product: {productServices.GetProductNameById(od.ProductID)}");
                MessageInsideBox($"Unit price: {String.Format("{0:0.00}", od.UnitPrice)}");
                MessageInsideBox($"Quantity: {od.Quantity}");
                MessageInsideBox($"Discount: {od.Discount}");
                MessageInsideBox($"Subtotal: {String.Format("{0:0.00}", getOutput.Subtotal(od))}");
            }
            Line();
            MessageInsideBox($"Total(with freight): {String.Format("{0:0.00}", getOutput.Total(order))}");

            ImportantMessage("Press any key to continue...");
        }
        private static void CreateOrder()
        {
            var getInput = new GetInput();
            var newOrderDTO = new OrderDTO
            {
                CustomerID = getInput.CustomerID("Please enter the customer ID: "),
                EmployeeID = getInput.EmployeeID(),
                OrderDate = getInput.Date("Please enter the order date: "),
                RequiredDate = getInput.Date("Please enter the required date: "),
                ShippedDate = getInput.Date("Please enter the shipped date: "),
                ShipVia = getInput.ShipVia("Please enter the ship via: "),
                Freight = getInput.PositiveOrZeroDecimal("Please enter the freight: "),
                ShipName = getInput.ValidString("Please enter the ship name: ", 40),
                ShipAddress = getInput.ValidString("Please enter the ship address: ", 60),
                ShipCity = getInput.ValidString("Please enter the ship city: ", 15),
                ShipRegion = getInput.ValidString("Please enter the ship region: ", 15),
                ShipPostalCode = getInput.ValidString("Please enter the ship postal code: ", 10),
                ShipCountry = getInput.ValidString("Please enter the ship country: ", 15),
            };
            
            var orderServices = new OrderServices();

            newOrderDTO.OrderID = orderServices.Create(newOrderDTO);

            getInput.OrderDetails(newOrderDTO.OrderID);

            var getOutput = new GetOutput();
            var amount = getOutput.Total(newOrderDTO);
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
            StandBy();
        }
        private static string StandBy()
        {
            Console.Write("\n > ");
            return Console.ReadLine();
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
