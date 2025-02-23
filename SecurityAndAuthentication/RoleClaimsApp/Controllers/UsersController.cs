using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RoleClaimsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpGet("role-based")]
        public IActionResult GetUserByRole()
        {
            ClaimsPrincipal user = new (new ClaimsIdentity([
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Role, "Admin")
            ], "mock"));
            
            HttpContext.User = user;
            
            if (User.IsInRole("Admin"))
            {
                return Ok(new { message = "Access granted for admin role." });
            }
            else
            {
                return Forbid();
            }
        }
        
        [HttpGet("claim-based")]
        public IActionResult GetUserByClaim()
        {
            ClaimsPrincipal user = new (new ClaimsIdentity([
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim("Department", "IT")
            ], "mock"));
            
            HttpContext.User = user;
            
            if (User.HasClaim("Department", "IT"))
            {
                return Ok(new { message = "Access granted for IT department." });
            }
            else
            {
                return Forbid();
            }
        }
    }
}