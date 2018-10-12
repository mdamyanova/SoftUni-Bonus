namespace SoftUni.WebServer.Http.Cookies
{
    using Common;
    using System;

    public class HttpCookie
    {
        private string key;
        private string value;

        public HttpCookie(string key, string value, int expires = 3)
        {
            // The 'expires' parameter is in days
            this.Key = key;
            this.Value = value;
            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(string key, string value, bool isNew, int expires = 3)
            : this(key, value, expires)
        {
            this.IsNew = isNew;
        }

        public string Key
        {
            get
            {
                return this.key;
            }

            private set
            {
                Validation.EnsureNotNullOrEmptyString(value, nameof(this.Key), "The key is required.");
                this.key = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }

            private set
            {
                Validation.EnsureNotNullOrEmptyString(value, nameof(this.Value), "The value is required.");
                this.value = value;
            }
        }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; private set; } = true;

        public override string ToString()
            => $"{this.Key}={this.Value}; Expires={this.Expires.ToLongTimeString()}";
    }
}