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
        public OrderDTO GetOrderById(int id)
        {
            var orderServices = new OrderServices();

            OrderDTO orderDto = orderServices.Find(id);
        }
    }
}
