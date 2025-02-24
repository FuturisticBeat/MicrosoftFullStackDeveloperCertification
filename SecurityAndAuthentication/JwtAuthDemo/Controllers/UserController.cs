using JwtAuthDemo.Models;
using JwtAuthDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private static readonly List<User> Users =
        [
            new User { Username = "testuser", Password = "password123" }
        ];

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            User? existingUser = Users.SingleOrDefault(u => u.Equals(user));

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            string token = _tokenService.GenerateToken(user.Username);
            return Ok(new { Token = token });
        }

        [HttpGet("secure-data")]
        [Authorize]
        public IActionResult GetSecureData()
        {
            return Ok(new { Message = "This is a protected endpoint!" });
        }
    }
}