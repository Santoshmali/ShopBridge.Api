using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Api.Models.Catalog;
using ShopBridge.Core.Entities.Catalog;
using ShopBridge.Core.Extensions;
using ShopBridge.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<Product> Get(int id)
        {
            return  await _productService.GetProductByProductId(id);
        }

        [HttpPost]
        public async Task<Product> CreateProduct(Product product)
        {
            product.ThrowIfNull(nameof(product));
            return await _productService.Insert(product);
        }
    }
}
