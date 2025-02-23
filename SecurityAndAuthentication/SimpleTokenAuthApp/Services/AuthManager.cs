using SimpleTokenAuthApp.Models;

namespace SimpleTokenAuthApp.Services
{
    public class AuthManager
    {
        private readonly List<User> _users = [];
        private readonly TokenManager _tokenManager = new();

        public void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;
        
            if (_users.Any(u => u?.Username == username))
            {
                Console.WriteLine("Error: User already exists.");
                return;
            }
        
            User newUser = new()
            {
                Username = username,
                Password = password,
                Token = _tokenManager.GenerateToken(username)
            };
        
            _users.Add(newUser);
            Console.WriteLine("Success: User registered successfully.");
        }
        
        public void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;
        
            User? user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Error: Invalid credentials.");
                return;
            }
        
            user.Token = _tokenManager.GenerateToken(username);
            Console.WriteLine($"Success: User logged in successfully. Token: {user.Token}");
        }

        public User? GetUserByToken(string token)
        {
            return _users.FirstOrDefault(u => u.Token == token);
        }
    }
}