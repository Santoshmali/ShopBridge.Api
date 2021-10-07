using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Data.Pagination;
using System.Threading.Tasks;

namespace ShopBridge.Services.Catalog
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductModel>> GetProductByProductIdAsync(int id);
        Task<ServiceResponse<ProductModel>> InsertAsync(ProductCreateRequest productCreateRequest);
        Task<ServiceResponse<PagedList<ProductModel>>> GetAllAsync(string searchtext, PaginationParameters parameters);
        Task<ServiceResponse<ProductModel>> UpdateAsync(ProductUpdateRequest productUpdateRequest);
        Task<ServiceResponse<bool>> DeleteProductById(int id);
    }
}
