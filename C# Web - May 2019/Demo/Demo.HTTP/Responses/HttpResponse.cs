﻿namespace Demo.HTTP.Responses
{
    using System.Linq;
    using System.Text;

    using Common;
    using Contracts;
    using Cookies;
    using Cookies.Contracts;
    using Enums;
    using Headers;
    using Headers.Contracts;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode)
            : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set;  }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public byte[] Content { get; set; }
        
        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));
            this.Cookies.AddCookie(cookie);
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            // HTTP/1.1 200 OK
            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusCode.ToString()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

            if (this.Cookies.HasCookies())
            {
                result.Append($"Set-Cookie: {this.Cookies}").Append(GlobalConstants.HttpNewLine);
            }

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}