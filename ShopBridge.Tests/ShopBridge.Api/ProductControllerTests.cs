using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Moq;
using ShopBridge.Api.Controllers;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Services.Catalog;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Tests.ShopBridge.Api
{
    public class ProductControllerTests
    {
        // Unit under test
        ProductController _productController;

        //Dependencies
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IFeatureManager> _featureManagerMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _featureManagerMock = new Mock<IFeatureManager>();
            _loggerMock = new Mock<ILogger<ProductController>>();

            _productController = new ProductController(_productServiceMock.Object, _featureManagerMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_WithInvalidProductId_ShouldReturn400()
        {
            // Arrange
            int id = 0;
            ServiceResponse<ProductModel> serviceResponse = new ServiceResponse<ProductModel>(System.Net.HttpStatusCode.NotFound, null);
            _productServiceMock.Setup(x => x.GetProductByProductIdAsync(id)).Returns(Task.FromResult(serviceResponse));

            // Act
            var result = await _productController.Get(id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ServiceResponse<ProductModel>>(objectResult.Value);
            Assert.Equal(404, model.HttpStatusCode);

        }
    }
}
