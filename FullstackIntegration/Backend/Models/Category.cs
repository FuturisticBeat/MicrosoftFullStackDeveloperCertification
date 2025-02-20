using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    
    /// <summary>
    /// Represents a category with an ID and name.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public required string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the products associated with the category.
        /// This is a navigation property used by Entity Framework Core.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}