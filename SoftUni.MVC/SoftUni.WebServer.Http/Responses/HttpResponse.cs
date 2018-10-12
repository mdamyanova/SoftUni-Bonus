namespace SoftUni.WebServer.Http.Responses
{
    using Cookies;
    using Headers;
    using Http.Enums;
    using Interfaces;
    using System.Text;

    public abstract class HttpResponse : IHttpResponse
    {
        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        protected HttpResponse(HttpStatusCode statusCode)
            : this()
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string StatusCodeDescription => this.StatusCode.ToString();

        public IHttpHeaderCollection Headers { get; private set; }

        public IHttpCookieCollection Cookies { get; private set; }

        public void AddHeader(string key, string value)
        {
            this.Headers.Add(new HttpHeader(key, value));
        }

        public override string ToString()
        {
            var response = new StringBuilder();
            response.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCodeDescription}")
                .AppendLine(this.Headers.ToString())
                .AppendLine();
            return response.ToString();
        }
    }
}