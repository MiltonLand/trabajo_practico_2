using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Services
{
    public class CustomerServices
    {
        private Repository<Customer> _customerRepository;

        public CustomerServices()
        {
            _customerRepository = new Repository<Customer>();
        }

        public bool ValidCustomerID(string id)
        {
            var cust = _customerRepository
                .Set()
                .FirstOrDefault(c => c.CustomerID == id);

            return (cust != null);
        }
        public string GetName(string id)
        {
            var customer = _customerRepository
                .Set()
                .FirstOrDefault(c => c.CustomerID == id);

            return customer.ContactName;
        }

        //Returns a list of a tuple("country", "customer name")
        //which represents a list of the biggest buyers in each country.
        public List<Tuple<string, string>> BiggestBuyersByCountry()
        {
            var customersByCountry = _customerRepository.Set().GroupBy(c => c.Country);

            decimal mostSpentByCountry;
            decimal actualValue;

            string biggestBuyerInCountry;

            var results = new List<Tuple<string, string>>();

            foreach (var customerCountry in customersByCountry)
            {
                mostSpentByCountry = 0;
                biggestBuyerInCountry = "No one";
                foreach (var c in customerCountry)
                {
                    actualValue = this.TotalMoneySpent(c.CustomerID);

                    if (actualValue > mostSpentByCountry)
                    {
                        mostSpentByCountry = actualValue;
                        biggestBuyerInCountry = c.ContactName;
                    }
                }

                results.Add(Tuple.Create(customerCountry.Key, biggestBuyerInCountry));
            }

            return results;
        }
        public decimal TotalMoneySpent(string id)
        {
            var customer = _customerRepository
                .Set()
                .FirstOrDefault(c => c.CustomerID == id);

            if (customer == null)
                return 0;

            var orderDetailServices = new OrderDetailServices();

            decimal grandTotal = 0;

            foreach (var o in customer.Orders)
            {
                grandTotal += orderDetailServices.Total(o.OrderID);
            }

            return grandTotal;
        }
    }
}
