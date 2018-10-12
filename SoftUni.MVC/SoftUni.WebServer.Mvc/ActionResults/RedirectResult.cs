namespace SoftUni.WebServer.Mvc.ActionResults
{
    using Interfaces;

    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; }

        public string Invoke()
            => this.RedirectUrl;
    }
}