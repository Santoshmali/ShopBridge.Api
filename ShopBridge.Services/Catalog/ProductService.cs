using AutoMapper;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Core.Extensions;
using ShopBridge.Data.Catalog;
using ShopBridge.Data.DbModels.Catalog;
using System.Collections.Generic;
using System.Net;
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

        public async Task<ServiceResponse<List<ProductModel>>> GetAllAsync(string searchtext = "")
        {
            var product = await  _productRepository.GetAllAsync(searchtext);
            var productModelList =  _mapper.Map<List<ProductModel>>(product);
            return new ServiceResponse<List<ProductModel>>(HttpStatusCode.OK, _mapper.Map<List<ProductModel>>(productModelList));
        }

        public async Task<ServiceResponse<ProductModel>> GetProductByProductIdAsync(int id)
        {
            var product = await _productRepository.GetProductByProductId(id);          
            var productModel = _mapper.Map<ProductModel>(product);

            if (productModel.IsNull())
            {
                return new ServiceResponse<ProductModel>(HttpStatusCode.NotFound, null);
            }
            return new ServiceResponse<ProductModel>(HttpStatusCode.OK, _mapper.Map<ProductModel>(productModel));
        }

        public async Task<ServiceResponse<ProductModel>> InsertAsync(ProductCreateRequest productCreateRequest)
        {
            productCreateRequest.ThrowIfNull(nameof(productCreateRequest));
            var product = await _productRepository.Insert(_mapper.Map<Product>(productCreateRequest));
            var productModel = _mapper.Map<ProductModel>(product);

            return new ServiceResponse<ProductModel>(HttpStatusCode.OK, _mapper.Map<ProductModel>(productModel));
        }        
    }
}
