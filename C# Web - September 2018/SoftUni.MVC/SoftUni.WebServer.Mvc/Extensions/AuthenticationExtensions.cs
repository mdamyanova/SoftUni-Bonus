namespace SoftUni.WebServer.Mvc.Extensions
{
    using Http.Interfaces;
    using Http.Session;
    using System.Collections.Generic;

    public static class AuthenticationExtensions
    {     
        public static void SignIn(string username, string userId, 
            IEnumerable<string> roles, IHttpRequest request)
        {
           request.Session.Add(SessionStore.SessionUserIdKey, userId);
           request.Session.Add(SessionStore.SessionUsernameKey, username);
           request.Session.Add(SessionStore.SessionUserRolesKey, roles);
        }

        public static void SignIn(string username, int userId, 
            IEnumerable<string> roles, IHttpRequest request)
        {
            request.Session.Add(SessionStore.SessionUserIdKey, userId);
            request.Session.Add(SessionStore.SessionUsernameKey, username);
            request.Session.Add(SessionStore.SessionUserRolesKey, roles);
        }

        public static void SignOut(IHttpRequest request)
        {
            request.Session.Clear();
        }
    }
}