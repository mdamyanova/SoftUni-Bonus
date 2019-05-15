namespace SoftUni.WebServer.Server.Interfaces
{
    using Http.Interfaces;

    public interface IHttpRequestHandler
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}