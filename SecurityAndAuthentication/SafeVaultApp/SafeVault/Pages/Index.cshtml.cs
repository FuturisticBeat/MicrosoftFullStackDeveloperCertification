using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.Models;
using SafeVault.Utilities;

namespace SafeVault.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }
    
    [BindProperty]
    public string Password { get; set; }
    
    public bool LoginFailed { get; set; }
    
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _applicationDbContext = dbContext;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostSubmit()
    {
        string allowedSpecialCharacters = "!@#$%^&*?";

        if (!ValidationHelpers.IsValidInput(Username) ||
            !ValidationHelpers.IsValidInput(Password, allowedSpecialCharacters))
        {
            LoginFailed = true;
            return Page();
        }

        User? existingUser = await _applicationDbContext.Users.SingleOrDefaultAsync(user => user.Username == Username
                                                                       && user.Password == Password);

        if (existingUser != null)
        {
            // Redirect to a secure page on successful login
            return RedirectToPage("/SecurePage");
        }

        // Set the LoginFailed flag to display an error message
        LoginFailed = true;
        return Page();
    }
}