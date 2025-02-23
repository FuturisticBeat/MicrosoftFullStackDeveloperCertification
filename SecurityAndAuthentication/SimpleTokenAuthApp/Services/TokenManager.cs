using System.Globalization;
using System.Text;

namespace SimpleTokenAuthApp.Services
{
    public class TokenManager
    {
        public string GenerateToken(string userName)
        {
            string expiry = DateTime.Now.AddMinutes(30).ToString(CultureInfo.InvariantCulture);
            string tokenData = $"{userName}:{expiry}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenData));
        }
    }
}
