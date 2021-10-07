using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.DbModels.Users;

namespace ShopBridge.Data.Context
{
    public class ShopBridgeDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options)
            : base(options)
        {
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("ShopBridgeProduct");
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<User>().ToTable("ShopBridgeUser");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasMany(x => x.RefreshTokens);

            modelBuilder.Entity<UserRefreshToken>().ToTable("ShopBridgeUserRefreshToken");
            modelBuilder.Entity<UserRefreshToken>().HasKey(x => x.Id);
            modelBuilder.Entity<UserRefreshToken>().HasOne(x => x.User).WithMany(u => u.RefreshTokens).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRefreshToken>().Ignore(x => x.IsActive);
            modelBuilder.Entity<UserRefreshToken>().Ignore(x => x.IsExpired);

            base.OnModelCreating(modelBuilder);
        }
    }
}
