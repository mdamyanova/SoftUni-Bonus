namespace SoftUni.WebServer.Http
{
    using Interfaces;
    using Requests;

    public class HttpContext : IHttpContext
    {
        public HttpContext(string requestString)
        {
            this.Request = new HttpRequest(requestString);
        }

        public IHttpRequest Request { get; private set; }
    }
}