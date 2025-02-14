using Microsoft.EntityFrameworkCore;

namespace CRUDWithMySQL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=ProductDB;User=root;Password=root;",
                new MySqlServerVersion(new Version(8, 0, 41)));
        }
    }
}