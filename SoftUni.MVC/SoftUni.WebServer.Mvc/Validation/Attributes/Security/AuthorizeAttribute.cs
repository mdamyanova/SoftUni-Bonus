namespace SoftUni.WebServer.Mvc.Attributes.Security
{
    using Common;
    using Http.Interfaces;
    using Http.Responses;
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute
    {
        public virtual IHttpResponse GetResponse(string message)
        {
            return new RedirectResponse(Constants.LoginPath);
        }
    }
}