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
        public void DeleteByOrderID(int orderID)
        {
            var oDetails = _orderDetailRepository.Set().Where(o => o.OrderID == orderID);

            if (oDetails == null)
                return;

            foreach (var od in oDetails)
            {
                _orderDetailRepository.Remove(od);
            }

            _orderDetailRepository.SaveChanges();
        }
        public List<OrderDetailDTO> GetAllOrderDetails()
        {
            var oDetails = _orderDetailRepository.Set();

            var oDetailList = new List<OrderDetailDTO>();

            OrderDetailDTO oDetailDto;

            foreach (var od in oDetails)
            {
                oDetailDto = ConvertToDTO(od);
                oDetailList.Add(oDetailDto);
            }

            return oDetailList;
        }
        private Order_Detail ConvertToOrderDetail(OrderDetailDTO dto)
        {
            return new Order_Detail()
            {
                OrderID = dto.OrderID,
                ProductID = dto.ProductID,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity,
                Discount = dto.Discount
            };
        }
        private OrderDetailDTO ConvertToDTO(Order_Detail od)
        {
            return new OrderDetailDTO()
            {
                OrderID = od.OrderID,
                ProductID = od.ProductID,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Discount = od.Discount
            };
        }
    }
}
