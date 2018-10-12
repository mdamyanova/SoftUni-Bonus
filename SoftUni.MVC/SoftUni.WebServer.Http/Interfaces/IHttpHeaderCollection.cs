namespace SoftUni.WebServer.Http.Interfaces
{
    using Http.Headers;
    using System.Collections.Generic;

    public interface IHttpHeaderCollection : IEnumerable<HttpHeader>
    {
        void Add(HttpHeader header);

        bool ContainsKey(string key);

        HttpHeader Get(string key);
    }
}