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
            var cust = _customerRepository.Set().FirstOrDefault(c => c.CustomerID == id);

            if (cust == null)
                return false;

            return true;
        }
    }
}
