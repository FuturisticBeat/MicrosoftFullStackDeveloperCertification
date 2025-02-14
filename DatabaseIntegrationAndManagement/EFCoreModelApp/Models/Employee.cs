using System.ComponentModel.DataAnnotations;

namespace EFCoreModelApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        public DateTime HireDate { get; set; }
        
        // Foreign Key
        public int DepartmentID { get; set; }
        
        // Navigation Property
        public Department Department { get; set; }
    }
}