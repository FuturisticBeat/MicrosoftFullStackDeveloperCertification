using System.ComponentModel.DataAnnotations;

namespace SecureDataApp.Models
{
    public class User
    {
        [Required]
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}