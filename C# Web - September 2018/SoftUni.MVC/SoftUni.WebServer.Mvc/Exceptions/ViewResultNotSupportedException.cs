namespace SoftUni.WebServer.Mvc.Exceptions
{
    using Common;
    using System;

    public class ViewResultNotSupportedException : Exception
    {
        public ViewResultNotSupportedException()
            : base(Constants.ViewResultNotSupportedMessage)
        {
        }

        public ViewResultNotSupportedException(string message)
            : base(message)
        {
        }
    }
}