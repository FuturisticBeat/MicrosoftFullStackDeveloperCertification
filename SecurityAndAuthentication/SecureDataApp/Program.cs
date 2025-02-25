using System.Security.Cryptography;
using SecureDataApp.Models;

namespace SecureDataApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Create users
            User admin = new (){ Username = "AdminUser", Role = "Admin" };
            User user = new (){ Username = "BasicUser", Role = "User" };
            
            // Initialize SecureStorage and set up encryption
            SecureStorage storage = new ();

            using Aes aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();
            byte[] encryptionKey = aes.Key;
            byte[] initializationVector = aes.IV;

            // Store encrypted data
            storage.StoreData("Sensitive Information", aes.Key, aes.IV);
            
            // Attempt to retrieve data as Admin
            try
            {
                string adminData = storage.RetrieveData(admin, encryptionKey, initializationVector);
                Console.WriteLine($"Admin Access: {adminData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Admin Error: {ex.Message}");
            }
            
            // Attempt to retrieve data as Basic User
            try
            {
                string userData = storage.RetrieveData(user, encryptionKey, initializationVector);
                Console.WriteLine($"User Access: {userData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User Error: {ex.Message}");
            }
        }
    }
}