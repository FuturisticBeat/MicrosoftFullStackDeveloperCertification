using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });
            
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10m
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20m
                }
            );
        }
    }
}