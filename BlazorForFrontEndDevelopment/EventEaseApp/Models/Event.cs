using System.ComponentModel.DataAnnotations;

namespace EventEaseApp.Models
{
    public class Event
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is required")] 
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; }
    }
}