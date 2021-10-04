using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Data.DbModels.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Services.Catalog
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductModel>> GetProductByProductIdAsync(int id);
        Task<ServiceResponse<ProductModel>> InsertAsync(ProductCreateRequest productCreateRequest);
        Task<ServiceResponse<List<ProductModel>>> GetAllAsync(string searchtext = "");
    }
}
