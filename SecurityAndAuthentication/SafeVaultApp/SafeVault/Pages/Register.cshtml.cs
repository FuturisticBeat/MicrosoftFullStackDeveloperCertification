using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeVault.Pages;

/// <summary>
/// The RegisterModel class handles the registration functionality for the SafeVault application.
/// </summary>
public class RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    : PageModel
{
    /// <summary>
    /// Gets or sets the input model which contains the registration details.
    /// </summary>
    [BindProperty]
    public required InputModel Input { get; set; }

    /// <summary>
    /// The InputModel class contains the properties required for registration.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public required string Email { get; init; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; init; }

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; init; }
    }

    /// <summary>
    /// Handles the registration process when the form is submitted.
    /// </summary>
    /// <returns>An IActionResult representing the result of the registration attempt.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // If the form is not valid, redisplay the page.
            return Page();
        }
        
        // Create a new user with the provided email and password.
        IdentityUser user = new (){ UserName = Input.Email, Email = Input.Email };
        IdentityResult result = await userManager.CreateAsync(user, Input.Password);
        
        if (result.Succeeded)
        {
            // If registration is successful, add the user to the "User" role and sign them in.
            await userManager.AddToRoleAsync(user, "User");
            await signInManager.SignInAsync(user, isPersistent: false);
            
            return RedirectToPage("Login");
        }
        
        // If registration fails, add errors to the ModelState.
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return Page();
    }
}
