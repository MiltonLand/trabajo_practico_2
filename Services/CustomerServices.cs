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
        private Repository<Customer> repository;

        public CustomerServices()
        {
            repository = new Repository<Customer>();
        }

        public bool ValidCustomerID(string id)
        {
            var cust = repository.Set().FirstOrDefault(c => c.CustomerID == id);

            if (cust == null)
                return false;

            return true;
        }
    }
}
