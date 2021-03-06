using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Context;
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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopBridgeDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByProductId(int id)
        {
            return await this.Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchtext"></param>
        /// <returns></returns>
        public PagedList<Product> GetAllAsync(string searchtext, PaginationParameters parameters)
        {
            // We cannot use x.Name.Contains(searchtext), as Contains function is in the string class 
            // and cannot be translated to SQL query by the linq provider
            // So we can use entity functions as mentioned below
            return PagedList<Product>.ToPagedList(Table.Where(x =>  
            string.IsNullOrEmpty(searchtext) ||
            EF.Functions.Like(x.Name, "%" + searchtext + "%") || 
            EF.Functions.Like(x.Details, "%" + searchtext + "%") ||
            EF.Functions.Like(x.Description, "%" + searchtext + "%")),parameters.PageNumber, parameters.PageSize);
        }
    }
}
