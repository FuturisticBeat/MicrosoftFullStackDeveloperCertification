using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    // added model validation using data annotations
    public class Item
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 300 characters.")]
        public string? Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Item otherItem &&
                   Name == otherItem.Name &&
                   Description == otherItem.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }
    }
}
