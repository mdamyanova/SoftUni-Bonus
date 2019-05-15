namespace SoftUni.WebServer.Http.Cookies
{
    using Common;
    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            Validation.EnsureNotNull(cookie, nameof(cookie), "The cookie to add must not be null.");
            this.cookies[cookie.Key] = cookie;
        }

        public void Add(string key, string value)
        {
            this.Add(new HttpCookie(key, value));
        }

        public bool ContainsKey(string key)
        {
            Validation.EnsureNotNullOrEmptyString(key, nameof(key));

            return this.cookies.ContainsKey(key);
        }

        public HttpCookie Get(string key)
        {
            if (!this.cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the cookies collection.");
            }

            return this.cookies[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}