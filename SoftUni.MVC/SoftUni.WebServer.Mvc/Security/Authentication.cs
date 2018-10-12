namespace SoftUni.WebServer.Mvc.Security
{
    using System.Collections.Generic;
    using System.Linq;

    public class Authentication
    {
        public Authentication()
        {
            this.IsAuthenticated = false;
        }

        public Authentication(object id, string name, IEnumerable<string> roles)
        {
            this.Id = id;
            this.Name = name;
            this.Roles = roles;
            this.IsAuthenticated = true;
        }

        public bool IsAuthenticated { get; private set; }

        public object Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<string> Roles { get; private set; }

        public bool IsInRole(string role)
        {
            return this.Roles.Contains(role);
        }
    }
}