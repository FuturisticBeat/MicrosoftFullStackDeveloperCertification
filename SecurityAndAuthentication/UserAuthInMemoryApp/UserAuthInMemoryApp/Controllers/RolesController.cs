using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UserAuthInMemoryApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        private async Task<IActionResult> InitializeRoles()
        {
            string[] roleNames = ["Admin", "User"];
            foreach (string roleName in roleNames)
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                {
                    continue;
                }
                IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Role already exists.");
                }
            }

            IdentityUser? user = await _userManager.FindByEmailAsync("admin@example.com");
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            
            bool isinRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (isinRole)
            {
                return Ok("Roles already created.");
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok("Roles created successfully.");
        }
    }
}