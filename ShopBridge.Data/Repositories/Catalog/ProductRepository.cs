using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Context;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Data.Catalog
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        public ProductRepository(IDbContext context) : base(context)
        {
        }

        public async Task<Products> GetProductByProductId(int id)
        {
            return await this.Table.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
