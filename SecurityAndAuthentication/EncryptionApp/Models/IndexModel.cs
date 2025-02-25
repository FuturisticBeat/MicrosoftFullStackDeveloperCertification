using EncryptionApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EncryptionApp.Models
{
    public class IndexModel : PageModel
    {
        private readonly EncryptionService _encryptionService;
        
        [BindProperty]
        public IFormFile? UploadedFile { get; set; }
        
        public IndexModel(EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void OnPostEncrypt()
        {
            if (UploadedFile == null)
            {
                return;
            }
            string inputPath = Path.Combine("wwwroot", UploadedFile.FileName);
            string encryptedPath = Path.Combine("wwwroot", $"encrypted_{UploadedFile.FileName}");
            using FileStream stream = new(inputPath, FileMode.Create);
            UploadedFile.CopyTo(stream);
            _encryptionService.EncryptFile(inputPath, encryptedPath);
        }

        public void OnPostDecrypt()
        {
            if (UploadedFile == null)
            {
                return;
            }
            
            string encryptedPath = Path.Combine("wwwroot", UploadedFile.FileName);
            string decryptedPath = Path.Combine("wwwroot", $"decrypted{UploadedFile.FileName}");
            _encryptionService.DecryptFile(encryptedPath, decryptedPath);
        }
    }
}