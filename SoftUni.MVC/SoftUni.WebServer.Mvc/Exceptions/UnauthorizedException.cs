namespace SoftUni.WebServer.Mvc.Exceptions
{
    using Common;
    using System;

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base(Constants.UnauthorizedMessage)
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}