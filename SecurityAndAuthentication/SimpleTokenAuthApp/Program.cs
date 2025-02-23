using SimpleTokenAuthApp.Services;

namespace SimpleTokenAuthApp;

class Program
{
    static void Main(string[] args)
    {
        AuthManager authManager = new AuthManager();
        SecureContentManager secureContentManager = new SecureContentManager(authManager);

        Console.WriteLine("Simple Token Auth App");
        while (true)
        {
            Console.WriteLine("Select an option: 1. Register, 2. Login, 3. Access Secure Content, 4. Exit");
            string selection = Console.ReadLine() ?? string.Empty;

            switch (selection)
            {
                case "1":
                    authManager.Register();
                    break;
                case "2":
                    authManager.Login();
                    break;
                case "3":
                    secureContentManager.AccessSecureContent();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }
}