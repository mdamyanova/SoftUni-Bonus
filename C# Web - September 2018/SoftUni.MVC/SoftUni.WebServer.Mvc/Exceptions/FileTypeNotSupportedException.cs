namespace SoftUni.WebServer.Mvc.Exceptions
{
    using Common;
    using System;

    public class FileTypeNotSupportedException : Exception
    {
        public FileTypeNotSupportedException()
            : base(Constants.FileTypeNotSupportedMessage)
        {
        }

        public FileTypeNotSupportedException(string message)
            : base(message)
        {
        }
    }
}