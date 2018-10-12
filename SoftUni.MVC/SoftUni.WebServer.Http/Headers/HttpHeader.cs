namespace SoftUni.WebServer.Http.Headers
{
    using Common;

    public class HttpHeader
    {
        public const string ContentType = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string ContentDisposition = "Content-Disposition";
        public const string Accept = "Accept";
        public const string Host = "Host";
        public const string Location = "Location";
        public const string Cookie = "Cookie";
        public const string SetCookie = "Set-Cookie";

        private string key;
        private string value;

        public HttpHeader(string key, string value)
        {
            this.Key = key;
            this.Value = value;
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

        public override string ToString() => $"{this.Key}: {this.Value}";
    }
}