namespace EventEaseApp.Models
{
    // Models/UserSession.cs
    public class UserSession
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsLoggedIn { get; set; }
        // Add any other session-related properties
    }
}