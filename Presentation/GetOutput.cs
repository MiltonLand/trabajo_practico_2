using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class GetOutput
    {
        public OrderDTO GetOrderByID(int id)
        {
            var orderServices = new OrderServices();

            OrderDTO orderDto = orderServices.Find(id);

            return orderDto;
        }
        public decimal Subtotal(OrderDetailDTO od)
        {
            var orderDetailServices = new OrderDetailServices();

            return orderDetailServices.Subtotal(od);
        }
        public decimal Total(OrderDTO orderDto)
        {
            var orderDetailServices = new OrderDetailServices();

            decimal total = (decimal)orderDto.Freight;

            total += orderDetailServices.Total(orderDto.OrderID);

            return total;
        }
        public List<OrderDetailDTO> GetOrderDetailsByID(int id)
        {
            var oDetailServices = new OrderDetailServices();
            var oDetailList = oDetailServices.GetDetailByID(id);

            return oDetailList;
        }

    }
}
