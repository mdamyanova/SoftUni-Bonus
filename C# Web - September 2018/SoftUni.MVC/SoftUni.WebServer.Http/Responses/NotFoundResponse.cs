namespace SoftUni.WebServer.Http.Responses
{
    using Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
            : base(HttpStatusCode.NotFound)
        {
        }
    }
}