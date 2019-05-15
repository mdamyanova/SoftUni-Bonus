namespace SoftUni.WebServer.Http.Session
{
    using System.Collections.Concurrent;

    public class SessionStore
    {
        public const string SessionCookieKey = "SoftUniServerSessionID";
        public const string SessionUserIdKey = "UserID";
        public const string SessionUsernameKey = "Username";
        public const string SessionUserRolesKey = "Roles";
        
        private static ConcurrentDictionary<string, HttpSession> sessions = new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession GetSession(string id)
            => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}