using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Services.DTOs;

namespace Services
{
    public class OrderServices
    {
        private Repository<Order> _orderRepository;

        public OrderServices()
        {
            _orderRepository = new Repository<Order>();
        }

        public bool ValidId(int id)
        {
            var order = _orderRepository.Set().FirstOrDefault(o => o.OrderID == id);

            return (order != null);
        }
        //Takes an OrderDTO converts it to Order, validates it, and adds it to the database.
        //Returns id on success, null on failure.
        public int Create(OrderDTO orderDto)
        {
            if (!ValidOrder(orderDto))
                throw new Exception(message: "Error. Invalid order. ");

            var order = ConvertOrderDTO(orderDto);

            order = _orderRepository.Persist(order);
            _orderRepository.SaveChanges();

            return order.OrderID;
        }
        public OrderDTO Find(int id)
        {
            var order = _orderRepository.Set().FirstOrDefault(o => o.OrderID == id);

            if (order == null)
                return null;

            return ConvertOrderToDTO(order);
        }
        public void Update(OrderDTO orderDto)
        {
            var order = _orderRepository
                .Set()
                .FirstOrDefault(o => o.OrderID == orderDto.OrderID);

            order.OrderID = orderDto.OrderID;
            order.CustomerID = orderDto.CustomerID;
            order.EmployeeID = orderDto.EmployeeID;
            order.OrderDate = orderDto.OrderDate;
            order.RequiredDate = orderDto.RequiredDate;
            order.ShippedDate = orderDto.ShippedDate;
            order.ShipVia = orderDto.ShipVia;
            order.Freight = orderDto.Freight;
            order.ShipName = orderDto.ShipName;
            order.ShipAddress = orderDto.ShipAddress;
            order.ShipCity = orderDto.ShipCity;
            order.ShipRegion = orderDto.ShipRegion;
            order.ShipPostalCode = orderDto.ShipPostalCode;
            order.ShipCountry = orderDto.ShipCountry;

            _orderRepository.SaveChanges();
        }
        //Deletes an order. Returns true if it succeeds.
        public bool Delete(OrderDTO orderDto)
        {
            var order = _orderRepository
                .Set()
                .FirstOrDefault(o => o.OrderID == orderDto.OrderID);

            var orderDetailServices = new OrderDetailServices();

            if (order.Customer != null)
            {
                if ((order.Customer.Country != "Mexico") && (order.Customer.Country != "France"))
                {

                    orderDetailServices.DeleteByOrderID(order.OrderID);

                    _orderRepository.Remove(order);
                    _orderRepository.SaveChanges();
                    return true;
                }
            }
            else
            {

                orderDetailServices.DeleteByOrderID(order.OrderID);

                _orderRepository.Remove(order);
                _orderRepository.SaveChanges();
                return true;
            }


            return false;
        }

        public ProductDTO2 GetBestSellingProduct(string country)
        {
            var productServices = new ProductServices();
            var orderDetailServices = new OrderDetailServices();

            var productList = productServices.GetAllProducts();
            var orderDetailList = orderDetailServices.GetAllOrderDetails();
            

            var orderGroup = _orderRepository.Set().GroupBy(o => o.Customer.Country);

            var ordersInCountry = new List<Order>();

            foreach (var group in orderGroup)
            {
                foreach (var order in group)
                {
                    if (group.Key == country)
                    {
                        ordersInCountry.Add(order);
                    }
                }
            }

            foreach (var o in ordersInCountry)
            {
                foreach (var od in o.Order_Details)
                {
                    foreach (var p in productList)
                    {
                        if (p.ProductID == od.ProductID)
                        {
                            p.TotalQuantity += od.Quantity;
                        }
                    }
                }
            }

            decimal currentValue;
            decimal maxValue = 0;

            ProductDTO2 BestSellingProduct = null;

            foreach (var p in productList)
            {
                currentValue = p.TotalQuantity;
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    BestSellingProduct = p;
                }
            }

            return BestSellingProduct;
        }

        private bool ValidOrder(OrderDTO orderDto)
        {
            if (orderDto.CustomerID.Count() != 5) return false;

            var employees = new Repository<Employee>();
            var employee = employees.Set().FirstOrDefault(e => e.EmployeeID == orderDto.EmployeeID);

            if (employee == null) return false;
            if ((orderDto.ShipVia < 1) || (orderDto.ShipVia > 3))
            {
                return false;
            }

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
            return new Order
            {
                OrderID = orderDto.OrderID,
                CustomerID = orderDto.CustomerID,
                EmployeeID = orderDto.EmployeeID,
                OrderDate = orderDto.OrderDate,
                RequiredDate = orderDto.RequiredDate,
                ShippedDate = orderDto.ShippedDate,
                ShipVia = orderDto.ShipVia,
                Freight = orderDto.Freight,
                ShipName = orderDto.ShipName,
                ShipAddress = orderDto.ShipAddress,
                ShipCity = orderDto.ShipCity,
                ShipRegion = orderDto.ShipRegion,
                ShipPostalCode = orderDto.ShipPostalCode,
                ShipCountry = orderDto.ShipCountry
            };
        }
        private OrderDTO ConvertOrderToDTO(Order order)
        {
            return new OrderDTO
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                EmployeeID = order.EmployeeID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry
            };
        }
    }
}
