using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SafeVault.Pages;

/// <summary>
/// The LoginModel class handles the login functionality for the SafeVault application.
/// </summary>
public class LoginModel(SignInManager<IdentityUser> signInManager) : PageModel
{
    /// <summary>
    /// Gets or sets the input model which contains the login details.
    /// </summary>
    [BindProperty]
    public required InputModel Input { get; set; }

    /// <summary>
    /// The InputModel class contains the properties required for login.
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
    }

    /// <summary>
    /// Handles the login process when the form is submitted.
    /// </summary>
    /// <returns>An IActionResult representing the result of the login attempt.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // If the form is not valid, redisplay the page.
            return Page();
        }
        
        // Attempt to sign in the user with the provided email and password.
        SignInResult result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            // If the login is successful, redirect to the secure page.
            return RedirectToPage("SecurePage");
        }
        
        // If the login attempt is invalid, add an error message to the ModelState.
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}
