namespace SoftUni.WebServer.Mvc.Attributes.HttpMethods
{
    using Common; 

    public class HttpPostAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
            => requestMethod.ToUpper() == Constants.PostMethodUpper;
    }
}