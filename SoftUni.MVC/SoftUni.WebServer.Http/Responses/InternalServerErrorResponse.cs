namespace SoftUni.WebServer.Http.Responses
{
    using Enums;
    using System;

    public class InternalServerErrorResponse : ContentResponse
    {
        public InternalServerErrorResponse(Exception exception)
            : base(HttpStatusCode.InternalServerError, exception.Message)
        {
        }
    }
}