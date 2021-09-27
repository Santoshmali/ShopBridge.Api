using Microsoft.EntityFrameworkCore;
using ShopBridge.Core.Entities.Catalog;
using ShopBridge.Data;
using ShopBridge.Data.Catalog;
using ShopBridge.Data.Context;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByProductId(int id)
        {
            //return await _productRepository.GetProductByProductId(id);
            return null;
        }
    }
}
