using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDemo.Services
{
    public class TokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly double _expiryInMinutes;
        
        public TokenService(IConfiguration configuration)
        {
            _key = configuration["JwtSettings:Key"] ?? 
                   throw new InvalidOperationException("Jwt key not set");
            
            _issuer = configuration["JwtSettings:Issuer"] ?? 
                      throw new InvalidOperationException("Jwt issuer not set");
            
            _audience = configuration["JwtSettings:Audience"] ?? 
                      throw new InvalidOperationException("Jwt audience not set");

            if (!double.TryParse(configuration["JwtSettings:ExpiryInMinutes"], out _expiryInMinutes))
            {
                throw new InvalidOperationException("Jwt expiry not set or configured incorrectly");   
            }
        }

        public string GenerateToken(string username)
        {
            Claim[] claims =
            [
                new (JwtRegisteredClaimNames.Sub, username),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_key));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}