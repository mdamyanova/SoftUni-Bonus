namespace SoftUni.WebServer.Http.Responses
{
    using Enums;
    using System.Text;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }

        public ContentResponse(HttpStatusCode statusCode, string content)
            : this(statusCode)
        {
            this.Content = content;
        }

        public string Content { get; protected set; }

        public override string ToString()
        {
            var response = new StringBuilder(base.ToString());
            response.AppendLine(this.Content.ToString());
            return response.ToString();
        }
    }
}