using SimpleTokenAuthApp.Models;

namespace SimpleTokenAuthApp.Services
{
    public class SecureContentManager
    {
        private readonly AuthManager _authManager;

        public SecureContentManager(AuthManager authManager)
        {
            _authManager = authManager;
        }

        public void AccessSecureContent()
        {
            Console.Write("Enter token: ");
            string token = Console.ReadLine() ?? string.Empty;

            User? user = _authManager.GetUserByToken(token);
            if (user != null)
            {
                Console.WriteLine("Success: Access granted to secure content.");
            }
            else
            {
                Console.WriteLine("Error: Invalid token.");
            }
        }
    }
}