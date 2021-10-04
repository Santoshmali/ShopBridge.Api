using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using ShopBridge.Core;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Services.Catalog;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFeatureManager _featureManager;

        public ProductController(IProductService productService, IFeatureManager featureManager)
        {
            _productService = productService;
            _featureManager = featureManager;
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _productService.GetProductByProductIdAsync(id);
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await GetProducts();
        }

        [HttpGet]
        [Route("/search/{searchtext}")]
        public async Task<IActionResult> Search(string searchtext)
        {
            return await GetProducts(searchtext);
        }        

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest product)
        {
            var response = await _productService.InsertAsync(product);
            return StatusCode(response.HttpStatusCode, response);
        }

        #region private methods

        private async Task<IActionResult> GetProducts(string searchtext = "")
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlags.CacheEnabled.ToString()))
            {
                // Return data from cache, can be done using different techniques of caching
            }

            var response = await _productService.GetAllAsync(searchtext);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        #endregion
    }
}
