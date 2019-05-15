namespace SoftUni.WebServer.Mvc.Attributes.HttpMethods
{
    using Common;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
            => requestMethod.ToUpper() == Constants.GetMethodUpper;
    }
}