using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Presentation
{
    public class GetInput
    {
        public string CustomerID(string text)
        {
            string id;
            var services = new CustomerServices();
            do
            {
                id = GetCustomerID(text);
            } while (!services.ValidCustomerID(id));

            return id;
        }
        private string GetCustomerID(string text)
        {
            string input;
            do
            {
                Console.Write(text);
                input = Console.ReadLine();

            } while (input.Count() != 5);
            
            return input;
        }
        public int? EmployeeID()
        {
            int? eID;
            var services = new EmployeeServices();
            string eFirstName;
            string eLastName;
            do
            {
                eFirstName = this.ValidString("Please enter the employee's first name: ");
                eLastName = this.ValidString("Please enter the employee's last name: ");
                eID = services.FindEmployeeIdByName(eFirstName, eLastName);

                if (eID == null)
                    ImportantMessage("Employee not found. Try again.");

            } while (eID == null);

            return eID;
        }
        public string ValidString(string text, uint max = 50)
        {
            string input;
            do
            {
                Console.Clear();
                Console.Write(text);
                input = Console.ReadLine();

            } while ((input == "") || (input.Count() > max));

            return input;
        }
        public DateTime? Date(string text)
        {
            string input;
            DateTime rd;
            do
            {
                Console.Clear();
                Console.Write(text);
                input = Console.ReadLine();
                

            } while (!DateTime.TryParse(input, out rd));

            return rd;
        }
        public int ShipVia(string text)
        {
            var s = new ShipperServices();            
            int shipVia;
            do
            {
                Console.Clear();
                Console.Write(text);
                int.TryParse(Console.ReadLine(), out shipVia);

            } while (!s.ValidShipperID(shipVia));
            
            return shipVia;
        }
        public decimal? PositiveOrZeroDecimal(string text)
        {
            decimal? fr;
            decimal n;
            do
            {
                Console.Clear();
                Console.Write(text);
                decimal.TryParse(Console.ReadLine(), out n);

            } while (n < 0);

            fr = n;

            return fr;
        }
        public void OrderDetails(int id)
        {
            if (!ValidId(id)) return;

            var orderDetailServices = new OrderDetailServices();

            string input = "";
            bool firstOrder = true;
            do
            {
                if (input == "Y")
                {
                    ProductDTO product = this.GetProductByName();

                    short quantity = this.Quantity();

                    if (quantity >= product.UnitsInStock)
                        quantity = (short)product.UnitsInStock;

                    orderDetailServices.Create(new OrderDetailDTO
                    {
                        OrderID = id,
                        ProductID = product.ProductID,
                        UnitPrice = (decimal)product.UnitPrice,
                        Quantity = quantity,
                        Discount = this.Discount()
                    });

                    firstOrder = false;

                    ImportantMessage("Order Added. ");
                }

                Console.Clear();
                if (firstOrder)
                    Console.Write("Enter order? (Y/N) ");
                else
                    Console.WriteLine("Enter another order? (Y/N) ");

                input = Console.ReadLine().ToUpper();

            } while ((input == "Y") || (input != "N"));

            if (firstOrder == true)
                ImportantMessage("No receipt added. ");
        }
        public int OrderID()
        {
            var oServices = new OrderServices();

            int id;

            do
            {
                id = GetOrderID();

            } while (!oServices.ValidId(id));

            return id;
        }
        private int GetOrderID()
        {
            int id;

            do
            {
                Console.Clear();
                Console.Write("Enter the order ID: ");
                int.TryParse(Console.ReadLine(), out id);

            } while (id <= 0);

            return id;
        }
        private bool ValidId(int id)
        {
            var orderServices = new OrderServices();

            return orderServices.ValidId(id);
        }
        private float Discount()
        {
            float percentage;

            do
            {
                Console.Clear();
                Console.Write("Enter the discount: ");
                float.TryParse(Console.ReadLine(), out percentage);

            } while ((percentage < 0) || (percentage > 30));

            float discount = percentage / 100;

            return discount;
        }
        private ProductDTO GetProductByName()
        {
            var productServices = new ProductServices();
            ProductDTO product;
            string name;
            
            do
            {
                name = this.ValidString("Enter the product's name: ");
                product = productServices.GetProduct(name);

            } while (product == null);

            return product;
        }
        private short Quantity()
        {
            short quantity;

            do
            {
                Console.Clear();
                Console.Write("Enter the quantity: ");
                short.TryParse(Console.ReadLine(), out quantity);
            } while (quantity <= 0);

            return quantity;
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
            Console.Write("| " + text);
            if (length < lineLength)
            {
                for (int i = 0; i < lineLength - length - 1; i++)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("|");
        }
        private static void Line()
        {
            Console.WriteLine("+----------------------------------------------------------------+");
        }
    }
    

}
