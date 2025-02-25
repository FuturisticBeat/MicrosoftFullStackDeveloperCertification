using System.Security.Cryptography;

namespace SecureDataApp.Models
{
    public class SecureStorage
    {
        private string? _encryptedData;

        public void StoreData(string data, byte[] key, byte[] iv)
        {
            _encryptedData = Convert.ToBase64String(Encrypt(data, key, iv));
        }

        public string RetrieveData(User user, byte[] key, byte[] iv)
        {
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Access denied. Admin role required.");
            }

            if (string.IsNullOrWhiteSpace(_encryptedData))
            {
                throw new InvalidOperationException("Encrypted data not found.");
            }
            
            return Decrypt(Convert.FromBase64String(_encryptedData), key, iv);
        }

        private static byte[] Encrypt(string data, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new ();
            using CryptoStream cs = new (ms, encryptor, CryptoStreamMode.Write);
            using (StreamWriter writer = new (cs))
            {
                writer.Write(data);
            }

            return ms.ToArray();
        }

        private static string Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream ms = new (data);
            using CryptoStream cs = new (ms, decryptor, CryptoStreamMode.Read);
            using StreamReader reader = new (cs);
            return reader.ReadToEnd();
        }
    }
}