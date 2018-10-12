namespace SoftUni.WebServer.Mvc.Attributes.HttpMethods
{
    using System;

    public abstract class HttpMethodAttribute : Attribute
    {
        public abstract bool IsValid(string requestMethod);
    }
}