using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemo
{
    public class JwtDecoder
    {
        private const string SecretKey = "MySuperSecretKeyForThisDemoApp123456789"; // Key should be stored securely

        public void DecodeJwt(string token)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "JwtDemoApp",
                ValidAudience = "JwtDemoApp",
                IssuerSigningKey = securityKey
            };

            try
            {
                ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                Console.WriteLine("Decoded Claims:");
                foreach (Claim claim in principal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
            }
        }
    }
}