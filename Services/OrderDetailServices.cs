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
    }
}
