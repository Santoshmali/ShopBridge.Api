using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.DbModels.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Data.Context
{
    public class ShopBridgeDbContext : DbContext
    {
        //public ShopBridgeDbContext()
        //{
        //}

        public DbSet<Product> Products { get; set; }

        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options)
            : base(options)
        {
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("ShopBridgeProduct");
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        //public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        //{
        //    return base.Set<TEntity>();
        //}
    }
}
