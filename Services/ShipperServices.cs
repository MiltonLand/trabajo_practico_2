using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Services
{
    public class ShipperServices
    {
        private Repository<Shipper> repository;

        public ShipperServices()
        {
            repository = new Repository<Shipper>();
        }

        public bool ValidShipperID(int id)
        {
            var shipper = repository
                .Set()
                .FirstOrDefault(s => s.ShipperID == id);

            return (shipper != null);
        }
    }
}
