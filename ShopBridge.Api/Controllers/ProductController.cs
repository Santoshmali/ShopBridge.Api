using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using ShopBridge.Core;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Core.Extensions;
using ShopBridge.Services.Catalog;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFeatureManager _featureManager;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IFeatureManager featureManager, ILogger<ProductController> logger)
        {
            _productService = productService.ThrowIfNull(nameof(productService));
            _featureManager = featureManager.ThrowIfNull(nameof(featureManager));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _productService.GetProductByProductIdAsync(id);
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return await GetProducts();
        }

        [HttpGet]
        [AllowAnonymous]
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

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductById(id);
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest product)
        {
            var response = await _productService.UpdateAsync(product);
            return StatusCode(response.HttpStatusCode, response);
        }

        #region private methods

        private async Task<IActionResult> GetProducts(string searchtext = "")
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlags.CacheEnabled.ToString()))
            {
                // Return data from cache, can be done using different techniques of caching
                _logger.LogInformation("Cache is enabled, so reading data from cache service");
            }

            _logger.LogInformation("Cache is disabled, so reading data from database.");
            var response = await _productService.GetAllAsync(searchtext);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        #endregion
    }
}
