using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthExample.Services
{
    public class TokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiryInMinutes;
        
        public TokenService(IConfiguration configuration)
        {
            IConfiguration jwtSettings = configuration.GetSection("JwtSettings");
            _key = GetConfigValue("Key");
            _issuer = GetConfigValue("Issuer");
            _audience = GetConfigValue("Audience");
            _expiryInMinutes = GetConfigValue("ExpiryInMinutes");
            
            return;
            string GetConfigValue(string entryKey)
            {
                return jwtSettings[entryKey] ?? throw new InvalidOperationException($"Jwt {entryKey} is not set.");
            }
        }
        
        public string GenerateAccessToken(string username)
        {
            if (!double.TryParse(_expiryInMinutes, out double tokenDuration))
            {
                throw new InvalidOperationException("Jwt expiry is not configured properly.");
            }
            
            SymmetricSecurityKey key = new (Encoding.UTF8.GetBytes(_key));
            SigningCredentials credentials = new (key, SecurityAlgorithms.HmacSha256);
            
            Claim[] claims =
            [
                new (JwtRegisteredClaimNames.Sub, username),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];
            
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _issuer,
                Audience = _audience,
                Expires = DateTime.UtcNow.AddMinutes(tokenDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}