using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    // Services/UserSessionService.cs
    public class UserSessionService
    {
        private UserSession _userSession = new UserSession();

        public UserSession GetUserSession()
        {
            return _userSession;
        }

        public void SetUserSession(UserSession userSession)
        {
            _userSession = userSession;
        }

        public void ClearUserSession()
        {
            _userSession = new UserSession();
        }
    }
}