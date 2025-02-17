using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    /// <summary>
    /// Represents a product with an ID, name, description, and price.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public required string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(255)]
        public required string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; } = 10m;
    }
}