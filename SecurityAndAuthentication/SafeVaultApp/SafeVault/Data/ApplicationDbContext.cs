using Microsoft.EntityFrameworkCore;
using SafeVault.Models;

namespace SafeVault.Data
{
    /// <summary>
    /// Represents the database context for the SafeVault application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for Users.
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Configures the model for the ApplicationDbContext.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder being used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            });
            
            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "User1", Password = "Password1", Email = "user1@example.com" },
                new User { Id = 2, Username = "User2", Password = "Password2", Email = "user2@example.com" }
            );
        }
    }
}