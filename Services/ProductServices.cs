using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Services.DTOs;

namespace Services
{
    public class ProductServices
    {
        private Repository<Product> _productRepository;

        public ProductServices()
        {
            _productRepository = new Repository<Product>();
        }

        public bool ValidProduct(string productName)
        {
            var product = _productRepository
                .Set()
                .FirstOrDefault(p => p.ProductName == productName);

            return (product != null);
        }
        public ProductDTO GetProduct(string productName)
        {
            var product = _productRepository
                .Set()
                .FirstOrDefault(p => p.ProductName == productName);

            if (product == null) return null;

            return new ProductDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
        }
        public string GetProductNameById(int id)
        {
            var product = _productRepository.Set().FirstOrDefault(p => p.ProductID == id);

            return product.ProductName;
        }
        public List<ProductDTO2> GetAllProducts()
        {
            var prodRepo = _productRepository.Set();

            var list = new List<ProductDTO2>();

            foreach (var p in prodRepo)
            {
                list.Add(new ProductDTO2
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    TotalQuantity = 0
                });
            }

            return list;
        }
    }
}
