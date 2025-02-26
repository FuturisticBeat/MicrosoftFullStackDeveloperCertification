using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SafeVault.Data
{
    /// <summary>
    /// Represents the database context for the SafeVault application.
    /// Uses ASP.NET Identity API for securing sensitive data
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<IdentityUser>(options);
}