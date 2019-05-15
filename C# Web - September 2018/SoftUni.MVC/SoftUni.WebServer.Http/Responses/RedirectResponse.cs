namespace SoftUni.WebServer.Http.Responses
{
    using Enums;
    using Headers;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            : base(HttpStatusCode.Found)
        {
            this.AddHeader(HttpHeader.Location, redirectUrl);
        }
    }
}