using Microsoft.AspNetCore.Mvc;
using ShopBridge.Api.Models.Catalog;
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
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            var product =  await _productService.GetProductByProductId(id);
            return null;
        }

        [HttpPost]
        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            var product = new ProductModel
            {
                Name = "Name",
                Description = "Desc",
                Price = 99.99m
            };

            //var product1= await _productService.Insert(new Core.Catalog.Product());

            return new ProductModel();
        }
    }
}
