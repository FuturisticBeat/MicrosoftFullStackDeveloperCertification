using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeVault.Pages;

/// <summary>
/// The IndexModel class handles the logic for the index page of the SafeVault application.
/// </summary>
public class IndexModel(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) : PageModel
{
    /// <summary>
    /// Gets the currently logged-in user, if any.
    /// </summary>
    public IdentityUser? CurrentUser { get; private set; }

    /// <summary>
    /// Called when a GET request is made to the index page.
    /// Checks if a user is authenticated and sets the CurrentUser property accordingly.
    /// </summary>
    public async Task OnGetAsync()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            CurrentUser = await userManager.GetUserAsync(User);
        }
        else
        {
            CurrentUser = null;
        }
    }
    
    /// <summary>
    /// Handles the logout process when the logout form is submitted.
    /// </summary>
    /// <returns>An IActionResult representing the result of the logout attempt.</returns>
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await signInManager.SignOutAsync();
        return RedirectToPage("Index");
    }
}