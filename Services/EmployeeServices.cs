using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Services
{
    public class EmployeeServices
    {
        private Repository<Employee> repository;

        public EmployeeServices()
        {
            repository = new Repository<Employee>();
        }
        
        public int? FindEmployeeIdByName(string fName, string lName)
        {
            var employee = repository.Set()
                .FirstOrDefault(e => e.FirstName == fName && e.LastName == lName);

            if (employee == null)
                return null;

            return employee.EmployeeID;
        }
    }
}
