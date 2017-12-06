using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Services
{
    public class OrderDetailServices
    {
        private Repository<Order_Detail> _orderDetailRepository;

        public OrderDetailServices()
        {
            _orderDetailRepository = new Repository<Order_Detail>();
        }
        public List<OrderDetailDTO> GetDetailByID(int orderID)
        {
            var orderDetails = _orderDetailRepository.Set().Where(o => o.OrderID == orderID);

            var list = new List<OrderDetailDTO>() { };
            foreach (var od in orderDetails)
            {
                list.Add(ConvertToDTO(od));
            }

            return list;
        }
        public decimal Subtotal(OrderDetailDTO od)
        {
            decimal subtotal = 0;
            decimal discount;

            subtotal = od.UnitPrice * od.Quantity;
            discount = subtotal * (decimal)od.Discount;
            subtotal -= discount;

            return subtotal;
        }
        public decimal Total(int id)
        {
            decimal total = 0;
            decimal subtotal;
            decimal discount;
            
            var orderDetails = _orderDetailRepository.Set().Where(od => od.OrderID == id);

            if (orderDetails == null) return 0;

            foreach (var od in orderDetails)
            {
                subtotal = od.UnitPrice * od.Quantity;
                discount = subtotal * (decimal)od.Discount;
                subtotal -= discount;
                total += subtotal;
            }

            return total;
        }
        public void Create(OrderDetailDTO orderDetailDto)
        {
            _orderDetailRepository.Persist(ConvertToOrderDetail(orderDetailDto));
            _orderDetailRepository.SaveChanges();
        }
        private Order_Detail ConvertToOrderDetail(OrderDetailDTO dto)
        {
            var orderDetail = new Order_Detail();

            orderDetail.OrderID = dto.OrderID;
            orderDetail.ProductID = dto.ProductID;
            orderDetail.UnitPrice = dto.UnitPrice;
            orderDetail.Quantity = dto.Quantity;
            orderDetail.Discount = dto.Discount;

            return orderDetail;
        }
        private OrderDetailDTO ConvertToDTO(Order_Detail od)
        {
            var orderDetailDTO = new OrderDetailDTO();

            orderDetailDTO.OrderID = od.OrderID;
            orderDetailDTO.ProductID = od.ProductID;
            orderDetailDTO.UnitPrice = od.UnitPrice;
            orderDetailDTO.Quantity = od.Quantity;
            orderDetailDTO.Discount = od.Discount;

            return orderDetailDTO;
        }
    }
}
