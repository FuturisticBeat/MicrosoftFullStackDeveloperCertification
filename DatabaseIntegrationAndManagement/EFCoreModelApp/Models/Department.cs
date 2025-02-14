using System.ComponentModel.DataAnnotations;

namespace EFCoreModelApp.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        // Navigation Property
        public List<Employee> Employees { get; set; }
    }
}