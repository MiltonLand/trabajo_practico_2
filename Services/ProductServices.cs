﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

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

            if (product == null)
                return false;
            return true;
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
    }
}
