namespace SoftUni.WebServer.Http.Interfaces
{
    using Enums;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        void AddHeader(string key, string value);
    }
}