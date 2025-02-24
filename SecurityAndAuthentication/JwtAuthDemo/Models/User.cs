using System.ComponentModel.DataAnnotations;

namespace JwtAuthDemo.Models
{
    public class User
    {
        [Required]
        public required string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not User otherUser)
            {
                return false;
            }
            
            return Username == otherUser.Username &&
                   Password == otherUser.Password;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(Username);
            hash.Add(Password);
            return hash.ToHashCode();
        }
    }
}