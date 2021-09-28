using ShopBridge.Core.Entities.Catalog;
using ShopBridge.Data;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Services.Catalog
{
    public interface IProductService
    {
        Task<Product> GetProductByProductId(int id);
        Task<Product> Insert(Product product);
    }
}
