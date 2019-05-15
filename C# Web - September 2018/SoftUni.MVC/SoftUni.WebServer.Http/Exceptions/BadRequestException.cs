namespace SoftUni.WebServer.Http.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        public BadRequestException()
            : base("The request is not valid.")
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}