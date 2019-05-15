namespace SoftUni.WebServer.Http.Requests
{
    using Common;
    using Cookies;
    using Enums;
    using Exceptions;
    using Headers;
    using Interfaces;
    using Session;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        private const int HttpRequestLinePartsLength = 3; // Method; URL; HTTP version
        private const string HttpVersion = "HTTP/1.1";
        private const string HeaderSeparator = ": ";
        private const string QueryStringSeparator = "?";
        private const string UrlParameterSeparator = "&";
        private const string UrlKeyValueSeparator = "=";

        private const string CookieSeparator = "; ";
        private const string CookieKeyValueSeparator = "=";

        private readonly string requestString;

        public HttpRequest(string requestString)
        {
            this.requestString = requestString;

            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public HttpRequestMethod Method { get; private set; }

        public string Url { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public IHttpCookieCollection Cookies { get; private set; }

        public IHttpSession Session { get; set; }

        public IDictionary<string, string> FormData { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            Validation.EnsureNotNullOrEmptyString(key, nameof(key));
            Validation.EnsureNotNullOrEmptyString(value, nameof(value));

            this.UrlParameters.Add(key, value);
        }

        public override string ToString() => this.requestString;

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString.Split(Environment.NewLine, StringSplitOptions.None);

            if (!requestLines.Any())
            {
                throw new BadRequestException();
            }

            string[] requestLineParts = requestLines[0]
                .Trim()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (requestLineParts.Length != HttpRequestLinePartsLength ||
                requestLineParts[HttpRequestLinePartsLength - 1].ToLower() != HttpVersion.ToLower())
            {
                throw new BadRequestException();
            }

            this.Method = this.ParseRequestMethod(requestLineParts[0]);
            this.Url = requestLineParts[1];
            this.Path = this.ParsePath(this.Url);

            var headerLines = requestLines
                .Skip(1)
                .TakeWhile(line => !string.IsNullOrEmpty(line));

            this.ParseHeaders(headerLines);
            this.ParseCookies();
            this.ParseParameters(this.Url);

            if (this.Method == HttpRequestMethod.Post)
            {
                this.FormData = this.ParseFormData(requestLines[requestLines.Length - 1]);
            }

            this.InitializeSession();
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            HttpRequestMethod methodResult;
            bool parsingSuccess = Enum.TryParse(method, true, out methodResult);

            if (!parsingSuccess)
            {
                throw new BadRequestException("The request method is invalid or not supported.");
            }

            return methodResult;
        }

        private string ParsePath(string url)
            => url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

        private void ParseHeaders(IEnumerable<string> headerLines)
        {
            foreach (string headerLine in headerLines)
            {
                string[] headerParts = headerLine.Split(HeaderSeparator);

                if (headerParts.Length != 2)
                {
                    throw new BadRequestException();
                }

                this.Headers.Add(new HttpHeader(headerParts[0], headerParts[1].Trim()));
            }

            if (!this.Headers.ContainsKey(HttpHeader.Host))
            {
                throw new BadRequestException("The headers must specify a host.");
            }
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsKey(HttpHeader.Cookie))
            {
                string allCookiesString = this.Headers.Get(HttpHeader.Cookie).Value;
                string[] cookies = allCookiesString.Split(CookieSeparator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    string[] cookieParts = cookie.Split(CookieKeyValueSeparator, StringSplitOptions.RemoveEmptyEntries);

                    if (cookieParts.Length != 2)
                    {
                        throw new BadRequestException();
                    }
                    
                    string key = cookieParts[0];
                    string value = cookieParts[1];
                    this.Cookies.Add(new HttpCookie(key, value, isNew: false));
                }
            }
        }

        private void ParseParameters(string url)
        {
            if (url.IndexOf(QueryStringSeparator) < 0)
            {
                return;
            }

            string query = url.Split(QueryStringSeparator, 2)[1];
            this.QueryParameters = this.ParseRequestParameters(query);
        }

        private IDictionary<string, string> ParseRequestParameters(string parametersString)
        {
            var requestParameters = new Dictionary<string, string>();
            string[] keyValuePairs = parametersString.Split(UrlParameterSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var keyValuePair in keyValuePairs)
            {
                string[] keyAndValue = keyValuePair.Split(UrlKeyValueSeparator, StringSplitOptions.RemoveEmptyEntries);

                if (keyAndValue.Length != 2)
                {
                    continue;
                }

                string key = WebUtility.UrlDecode(keyAndValue[0]);
                string value = WebUtility.UrlDecode(keyAndValue[1]);
                requestParameters[key] = value;
            }

            return requestParameters;
        }

        private IDictionary<string, string> ParseFormData(string query)
        {
            if (this.Method == HttpRequestMethod.Get)
            {
                return new Dictionary<string, string>();
            }

            try
            {
                return this.ParseRequestParameters(query);
            }
            catch(BadRequestException)
            {
                // It's OK to have fields with missing values, we need to just ignore them
                return null;
            }
        }

        private void InitializeSession()
        {
            if (this.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var cookie = this.Cookies.Get(SessionStore.SessionCookieKey);
                string sessionId = cookie.Value;

                this.Session = SessionStore.GetSession(sessionId);
            }
        }
    }
}