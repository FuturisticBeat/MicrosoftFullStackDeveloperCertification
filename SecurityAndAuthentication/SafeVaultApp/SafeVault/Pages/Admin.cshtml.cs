using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeVault.Pages
{
    /// <summary>
    /// Represents the page model for the Admin page in the SafeVault application.
    /// This page requires authorization to access.
    /// </summary>
    // Use Role-based authorization
    [Authorize(Roles = "Admin")]
    // OR
    // Use Policy-based authorization
    [Authorize(Policy = "CanAccessAdminPage")]
    public class AdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminModel"/> class with the specified user manager.
        /// </summary>
        /// <param name="userManager">The user manager used for user operations.</param>
        public AdminModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Gets or sets the list of users.
        /// </summary>
        public List<IdentityUser> Users { get; set; } = [];

        /// <summary>
        /// Gets or sets the dictionary containing user roles.
        /// </summary>
        public Dictionary<string, IList<string>> UserRoles { get; set; } = new();

        /// <summary>
        /// Handles GET requests to the Admin page.
        /// Populates the <see cref="Users"/> and <see cref="UserRoles"/> properties with user information.
        /// </summary>
        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            UserRoles = new Dictionary<string, IList<string>>();

            foreach (IdentityUser user in Users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                UserRoles[user.Id] = roles;
            }
        }
    }
}