namespace SoftUni.WebServer.Mvc.Helpers
{
    using Http.Interfaces;
    using Http.Session;
    using System.Collections.Generic;

    public static class AuthenticationHelper
    {
        public static object GetSessionUserId(IHttpRequest request)
           => request.Session.Get(SessionStore.SessionUserIdKey);

        public static string GetSessionUsername(IHttpRequest request)
            => request.Session.Get<string>(SessionStore.SessionUsernameKey);

        public static IEnumerable<string> GetSessionUserRoles(IHttpRequest request)
            => request.Session.Get<IEnumerable<string>>(SessionStore.SessionUserRolesKey);
    }
}