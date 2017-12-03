using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Services
{
    public class OrderServices
    {
        private Repository<Order> repository;

        public OrderServices()
        {
            repository = new Repository<Order>();
        }
        //Takes an OrderDTO converts it to Order, validates it, and adds it to the database.
        //Returns id on success, null on failure.
        public Nullable<int> AddNewOrder(OrderDTO orderDto)
        {
            if (!ValidOrder(orderDto)) return null;

            var order = ConvertOrderDTO(orderDto);

            order = repository.Persist(order);
            repository.SaveChanges();

            return order.OrderID;
        }
        private bool ValidOrder(OrderDTO orderDto)
        {
            if (orderDto.CustomerID.Count() != 5) return false;

            var employees = new Repository<Employee>();
            var employee = employees.Set().FirstOrDefault(e => e.EmployeeID == orderDto.EmployeeID);

            if (employee == null) return false;
            if ((orderDto.ShipVia < 1) || (orderDto.ShipVia > 3)) return false;
            if (orderDto.Freight < 0) return false;
            if (orderDto.ShipName.Count() > 40) return false;
            if (orderDto.ShipAddress.Count() > 60) return false;
            if (orderDto.ShipCity.Count() > 15) return false;
            if (orderDto.ShipRegion.Count() > 15) return false;
            if (orderDto.ShipPostalCode.Count() > 10) return false;
            if (orderDto.ShipCountry.Count() > 15) return false;


            return true;
        }
        private Order ConvertOrderDTO(OrderDTO orderDto)
        {
            var newOrder = new Order();

            newOrder.CustomerID = orderDto.CustomerID;
            newOrder.EmployeeID = orderDto.EmployeeID;
            newOrder.OrderDate = orderDto.OrderDate;
            newOrder.RequiredDate = orderDto.RequiredDate;
            newOrder.ShippedDate = orderDto.ShippedDate;
            newOrder.Freight = orderDto.Freight;
            newOrder.ShipName = orderDto.ShipName;
            newOrder.ShipAddress = orderDto.ShipAddress;
            newOrder.ShipCity = orderDto.ShipCity;
            newOrder.ShipRegion = orderDto.ShipRegion;
            newOrder.ShipPostalCode = orderDto.ShipPostalCode;
            newOrder.ShipCountry = orderDto.ShipCountry;

            return newOrder;
        }
    }
}
