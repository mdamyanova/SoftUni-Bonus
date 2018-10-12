namespace SoftUni.WebServer.Http.Exceptions
{
    using System;

    public class InvalidResponseException : Exception
    {
        public InvalidResponseException()
            : base("The response is not valid.")
        {
        }

        public InvalidResponseException(string message)
            : base(message)
        {   
        }
    }
}