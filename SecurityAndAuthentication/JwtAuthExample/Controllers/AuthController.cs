using JwtAuthExample.Models;
using JwtAuthExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        private static readonly Dictionary<string, string> Users = new()
        {
            { "user1", "password1" },
            { "admin", "password2" }
        };

        private static readonly Dictionary<string, string> RefreshTokens = new();

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!Users.TryGetValue(request.Username, out string? password) ||
                password != request.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            string accessToken = _tokenService.GenerateAccessToken(request.Username);
            string refreshToken = _tokenService.GenerateRefreshToken();
            
            RefreshTokens.Add(refreshToken, request.Username);

            return Ok(new { Message = "Login successful!", AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            // Validate and Invalidate old refresh token
            if (!RefreshTokens.Remove(request.RefreshToken, out string? username))
            {
                return Unauthorized("Invalid refresh token.");
            }
            
            // Generate new access token and refresh token
            string newAccessToken = _tokenService.GenerateAccessToken(username);
            string newRefreshToken = _tokenService.GenerateRefreshToken();
            
            // Store the new refresh token
            RefreshTokens.Add(newRefreshToken, username);

            return Ok(new
                { Message = "Refresh Successful!", AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }
    }
}