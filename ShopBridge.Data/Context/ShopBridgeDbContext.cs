using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.DbModels.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Data.Context
{
    public class ShopBridgeDbContext : DbContext, IDbContext
    {
        public ShopBridgeDbContext()
        {
        }

        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options)
            : base(options)
        {
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().ToTable("ShopBridgeProduct");
            modelBuilder.Entity<Products>().HasKey(x => x.ProductId);

            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
