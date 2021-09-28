using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Core.Entities.Catalog;
using ShopBridge.Core.Extensions;
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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository.ThrowIfNull(nameof(productRepository));
            _mapper = mapper.ThrowIfNull(nameof(mapper));
        }

        public async Task<Product> GetProductByProductId(int id)
        {
            var products = await _productRepository.GetProductByProductId(id);
            return _mapper.Map<Product>(products);
        }

        public async Task<Product> Insert(Product product)
        {
            product.ThrowIfNull(nameof(product));
            var products = await _productRepository.Insert(_mapper.Map<Products>(product));
            return _mapper.Map<Product>(products);
        }
    }
}
