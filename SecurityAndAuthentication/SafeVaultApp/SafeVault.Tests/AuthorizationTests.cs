using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SafeVault.Data;

namespace SafeVault.Tests;

/// <summary>
/// The AuthorizationTests class contains tests for verifying authorization in the SafeVault application.
/// </summary>
public class AuthorizationTests
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the AuthorizationTests class and configures necessary services.
    /// </summary>
    public AuthorizationTests()
    {
        // Create a new service collection to configure dependencies.
        ServiceCollection services = [];
        
        // Add an in-memory database context for testing.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        
        // Add Identity services with roles and Entity Framework stores.
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        // Add logging services.
        services.AddLogging();

        // Build the service provider and resolve the UserManager and RoleManager.
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    }
    
    /// <summary>
    /// Tests that access to the admin page fails for a user without the Admin role.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    [Trait("Category", "Authorization")]
    public async Task AccessAdminPage_WithoutAdminRole_ShouldFail()
    {
        // Arrange
        IdentityUser user = new() { UserName = "user@domain.com", Email = "user@domain.com" };
        await _userManager.CreateAsync(user, "UserPassword123!");

        // Act
        IList<string> roles = await _userManager.GetRolesAsync(user);

        // Assert
        Assert.DoesNotContain("Admin", roles);
    }
    
    /// <summary>
    /// Tests that a user with the Admin role has access.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    [Trait("Category", "Authorization")]
    public async Task UserWithAdminRole_ShouldHaveAccess()
    {
        // Arrange
        IdentityUser user = new() { UserName = "admin@domain.com", Email = "admin@domain.com" };
        await _userManager.CreateAsync(user, "AdminPassword123!");
        IdentityRole role = new("Admin");
        await _roleManager.CreateAsync(role);
        await _userManager.AddToRoleAsync(user, "Admin");

        // Act
        IList<string> roles = await _userManager.GetRolesAsync(user);

        // Assert
        Assert.Contains("Admin", roles);
    }
}
