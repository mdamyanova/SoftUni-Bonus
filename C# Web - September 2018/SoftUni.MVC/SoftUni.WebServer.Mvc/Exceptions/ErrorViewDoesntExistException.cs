namespace SoftUni.WebServer.Mvc.Exceptions
{
    using Common;
    using System;

    public class ErrorViewDoesntExistException : Exception
    {
        public ErrorViewDoesntExistException()
            : base(Constants.ErrorViewDoesntExistMessage)
        {
        }

        public ErrorViewDoesntExistException(string message)
            : base(message)
        {
        }
    }
}