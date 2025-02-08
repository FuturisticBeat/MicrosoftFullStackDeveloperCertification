using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Data;

public class UserDetails
{
    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? EmailAddress { get; set; }
}