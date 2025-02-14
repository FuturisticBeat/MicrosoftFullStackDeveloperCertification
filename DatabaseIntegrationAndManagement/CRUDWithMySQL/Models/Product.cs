using System.ComponentModel.DataAnnotations;

namespace CRUDWithMySQL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; } = 10m;
    }
}