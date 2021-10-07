using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.Pagination;
using ShopBridge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Data.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByProductId(int id);
        PagedList<Product> GetAllAsync(string searchtext, PaginationParameters parameters);
    }
}
