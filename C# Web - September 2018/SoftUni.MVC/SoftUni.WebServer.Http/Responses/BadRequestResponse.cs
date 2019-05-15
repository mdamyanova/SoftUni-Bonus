namespace SoftUni.WebServer.Http.Responses
{
    using Enums;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}