namespace SoftUni.WebServer.Http.Session
{
    using Common;
    using Interfaces;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> sessionItems;
        private string id;

        public HttpSession(string id)
        {
            this.Id = id;
            this.sessionItems = new Dictionary<string, object>();
        }

        public string Id
        {
            get
            {
                return this.id;
            }

            private set
            {
                Validation.EnsureNotNullOrEmptyString(value, nameof(this.Id), "The session ID is required.");
                this.id = value;
            }
        }

        public void Add(string key, object value)
        {
            Validation.EnsureNotNullOrEmptyString(key, nameof(key), "The key is required.");
            Validation.EnsureNotNull(value, nameof(value), "The value is required.");
            this.sessionItems.Add(key, value);
        }

        public void Clear()
        {
            this.sessionItems.Clear();
        }

        public bool Contains(string key)
        {
            Validation.EnsureNotNullOrEmptyString(key, nameof(key), "The key is required.");
            return this.sessionItems.ContainsKey(key);
        }

        public object Get(string key)
        {
            Validation.EnsureNotNullOrEmptyString(key, nameof(key), "The key is required.");

            if (!this.Contains(key))
            {
                return null;
            }

            return this.sessionItems[key];
        }

        public T Get<T>(string key)
        {
            return (T)this.Get(key);
        }
    }
}