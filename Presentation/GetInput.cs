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
        public string GetCustomerID(string text)
        {
            string input;
            do
            {
                Console.Write(text);
                input = Console.ReadLine();

            } while (input.Count() != 5);


            return input.ToUpper();
        }
        public Nullable<int> EmployeeID()
        {
            Nullable<int> eID;
            var services = new EmployeeServices();
            string eFirstName;
            string eLastName;
            do
            {
                eFirstName = this.ValidString("Please enter the employee's first name: ");
                eLastName = this.ValidString("Please enter the employee's last name: ");
                eID = services.findEmployeeIdByName(eFirstName, eLastName);
                if (eID == null)
                {
                    Console.Clear();
                    Console.WriteLine("Employee not found. Try again.");
                    Line();
                    Console.ReadLine();
                }

            } while (eID == null);

            return eID;
        }
        public string ValidString(string text, uint max = 50)
        {
            string input;
            do
            {
                Console.Write(text);
                input = Console.ReadLine();

            } while ((input == "") || (input.Count() > max));

            return input;
        }
        public Nullable<DateTime> Date(string text)
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
        public Nullable<int> ShipVia()
        {
            Nullable<int> sv;
            int n;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the ship via: ");
                int.TryParse(Console.ReadLine(), out n);

            } while ((n < 1) || (n > 3));

            sv = n;

            return sv;
        }
        public Nullable<decimal> positiveOrZeroDecimal()
        {
            Nullable<decimal> fr;
            decimal f;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the freight: ");
                decimal.TryParse(Console.ReadLine(), out f);

            } while (f < 0);

            fr = f;

            return fr;
        }
        public static void Line()
        {
            Console.WriteLine("/-----------------------------------------/");
        }
    }
    

}
