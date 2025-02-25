using System.Security.Cryptography;

namespace EncryptionApp.Services
{
    public class EncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionService(IConfiguration configuration)
        {
            IConfiguration encryptionSettings = configuration.GetSection("Encryption");
            _key = Convert.FromBase64String(encryptionSettings["Key"] ?? 
                                            throw new InvalidOperationException("Encryption key not set."));
            _iv = Convert.FromBase64String(encryptionSettings["IV"] ?? 
                                            throw new InvalidOperationException("Encryption iv not set."));
        }
        
        public void EncryptFile(string inputPath, string outputPath)
        {
            using Aes aes = Aes.Create();
            using ICryptoTransform encryptor = aes.CreateEncryptor(_key, _iv);
            using FileStream inputStream = new (inputPath, FileMode.Open);
            using FileStream outputStream = new (outputPath, FileMode.Create);
            using CryptoStream cryptoStream = new (outputStream, encryptor, CryptoStreamMode.Write);
            inputStream.CopyTo(cryptoStream);
        }

        public void DecryptFile(string inputPath, string outputPath)
        {
            using Aes aes = Aes.Create();
            using ICryptoTransform encryptor = aes.CreateDecryptor(_key, _iv);
            using FileStream inputStream = new (inputPath, FileMode.Open);
            using FileStream outputStream = new (outputPath, FileMode.Create);
            using CryptoStream cryptoStream = new (outputStream, encryptor, CryptoStreamMode.Read);
            cryptoStream.CopyTo(outputStream);
        }
    }
}