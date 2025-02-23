using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace OAuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : Controller
    {
        private static readonly Dictionary<string, string> AuthCodes = new();
        
        [HttpGet("authorize")]
        public IActionResult Authorize(string responseType, string clientId, string redirectUri, string state)
        {
            string authCode = Guid.NewGuid().ToString();
            AuthCodes.Add(authCode, clientId);
            return Redirect($"{redirectUri}?code={authCode}&state={state}");
        }
        
        [HttpPost("token")]
        public IActionResult Token([FromForm] string code,[FromForm] string clientId)
        {
            if(!AuthCodes.TryGetValue(code, out string? value) || value != clientId)
            {
                return BadRequest("Invalid code or client ID.");
            }
            
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.MRvO3fE0o9C-pZfd3pI0hMDDXihJfQa1XPQ-UAelzaI"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "OAuthServer",
                audience: clientId,
                claims: [new Claim("sub", "12345"), new Claim("name", "John Doe")],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                token_type = "Bearer",
                expires_in = 1800
            });
        }
    }
}