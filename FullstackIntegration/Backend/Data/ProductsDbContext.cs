using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)");
                
                entity.HasOne(c => c.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Category 1"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Category 2"
                }
            );
            
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10m,
                    Stock = 10,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20m,
                    Stock = 20,
                    CategoryId = 2
                }
            );
        }
    }
}