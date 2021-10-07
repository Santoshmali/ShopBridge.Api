using AutoMapper;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Core.Extensions;
using ShopBridge.Data.Catalog;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.Pagination;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ServiceResponse<bool>> DeleteProductById(int id)
        {
            // Check if id is not null and present in db
            var product = await _productRepository.GetProductByProductId(id);

            if(product.IsNull())
            {
                return new ServiceResponse<bool>(HttpStatusCode.NotFound, false);
            }

            await _productRepository.Delete(product);

            return new ServiceResponse<bool>(HttpStatusCode.OK, true);
        }

        public async Task<ServiceResponse<PagedList<ProductModel>>> GetAllAsync(string searchtext, PaginationParameters parameters)
        {
            // Get DbModel
            var product = _productRepository.GetAllAsync(searchtext, parameters);

            //NOTE: Automapper is not allowing to having generic mapping so workaround is to preapre pagedlist one more time
            // Data Model
            var productModelList = new PagedList<ProductModel>(_mapper.Map<List<ProductModel>>(product.Rows), product.TotalCount, product.CurrentPage, product.PageSize);

            return await Task.FromResult(new ServiceResponse<PagedList<ProductModel>>(HttpStatusCode.OK, productModelList));
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

        public async Task<ServiceResponse<ProductModel>> UpdateAsync(ProductUpdateRequest productUpdateRequest)
        {
            productUpdateRequest.ThrowIfNull(nameof(productUpdateRequest));

            // Check if id is not null and present in db
            var product = await _productRepository.GetProductByProductId(productUpdateRequest.Id);

            if (product.IsNull())
            {
                return new ServiceResponse<ProductModel>(HttpStatusCode.NotFound, null);
            }

            product = _mapper.Map(productUpdateRequest, product);

            var updatedProduct = await _productRepository.Update(product);
            var productModel = _mapper.Map<ProductModel>(updatedProduct);
            return new ServiceResponse<ProductModel>(HttpStatusCode.OK, _mapper.Map<ProductModel>(productModel));
        }
    }
}
