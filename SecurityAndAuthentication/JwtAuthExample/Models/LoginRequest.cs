using System.ComponentModel.DataAnnotations;

namespace JwtAuthExample.Models
{
    public class LoginRequest
    {
        [Required]
        public required string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}