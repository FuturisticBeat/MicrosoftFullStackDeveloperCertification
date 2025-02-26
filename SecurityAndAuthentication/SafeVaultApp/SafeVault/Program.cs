using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configures the ApplicationDbContext to use SQL Server with the specified connection string.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure Identity services with options.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        // Require unique email addresses for users.
        options.User.RequireUniqueEmail = true;
        
        // Configure password requirements.
        options.Password.RequireDigit = true; // Require at least one digit.
        options.Password.RequireLowercase = true; // Require at least one lowercase letter.
        options.Password.RequireUppercase = true; // Require at least one uppercase letter.
        options.Password.RequireNonAlphanumeric = true; // Require at least one non-alphanumeric character.
        options.Password.RequiredLength = 8; // Require a minimum password length of 8 characters.
    })
    .AddRoles<IdentityRole>() // Add support for role-based authorization.
    .AddEntityFrameworkStores<ApplicationDbContext>() // Use Entity Framework for storing identity data.
    .AddDefaultTokenProviders(); // Add default token providers for password reset and other operations.

// Add authorization policies.
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanAccessAdminPage", policy =>
        // Require the user to have the "Permission" claim with value "CanAccessAdminPage".
        policy.RequireClaim("Permission", "CanAccessAdminPage"));


// Add services to the container.
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Create a new scope for the services.
using (IServiceScope scope = app.Services.CreateScope())
{
    // Get the service provider from the scope.
    IServiceProvider services = scope.ServiceProvider;
    
    // Call the CreateRoles method to create roles asynchronously.
    await CreateRoles(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
return;

// Creates roles and a default admin user if they do not already exist.
async Task CreateRoles(IServiceProvider serviceProvider)
{
    // Get the RoleManager service to manage roles.
    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    // Define the roles to be created.
    string[] roleNames = ["Admin", "User"];

    // Iterate over each role name to check if it exists and create it if it does not.
    foreach (string roleName in roleNames)
    {
        bool roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Retrieve admin credentials from the configuration.
    IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string adminEmail = configuration["AdminCredentials:AdminEmail"] ??
                        throw new InvalidOperationException("Admin email is not set.");
    
    string adminPassword = configuration["AdminCredentials:AdminPassword"] ??
                           throw new InvalidOperationException("Admin password is not set.");

    // Create a default Admin user.
    UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    IdentityUser adminUser = new()
    {
        UserName = adminEmail,
        Email = adminEmail
    };

    // Check if the admin user already exists.
    IdentityUser? user = await userManager.FindByEmailAsync(adminEmail);

    if (user == null)
    {
        // If the admin user does not exist, create it with the provided password.
        IdentityResult createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdminUser.Succeeded)
        {
            // Add the admin user to the "Admin" role.
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
