namespace SoftUni.WebServer.Http.Headers
{
    using Common;
    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            Validation.EnsureNotNull(header, nameof(header), "The header to add must not be null.");
            
            // If the header exists, it will be replaced
            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
            => this.headers.ContainsKey(key);

        public HttpHeader Get(string key)
        {
            if (!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the header collection.");
            }

            return this.headers[key];
        }

        public IEnumerator<HttpHeader> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this);
        }
    }
}