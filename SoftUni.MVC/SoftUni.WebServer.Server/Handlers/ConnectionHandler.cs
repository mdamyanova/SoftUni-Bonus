namespace SoftUni.WebServer.Server.Handlers
{
    using Common;
    using Http.Headers;
    using Http.Interfaces;
    using Http.Requests;
    using Http.Responses;
    using Http.Session;
    using Server.Interfaces;
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IHttpRequestHandler mvcRequestHandler;
        private readonly IHttpRequestHandler fileHandler;

        public ConnectionHandler(
            Socket client,
            IHttpRequestHandler mvcRequestHandler,
            IHttpRequestHandler fileHandler)
        {
            Validation.EnsureNotNull(client, nameof(client));
            Validation.EnsureNotNull(mvcRequestHandler, nameof(mvcRequestHandler));
            Validation.EnsureNotNull(fileHandler, nameof(fileHandler));

            this.client = client;
            this.mvcRequestHandler = mvcRequestHandler;
            this.fileHandler = fileHandler;
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpResponse = this.HandleRequest(httpRequest);

                await this.PrepareResponse(httpResponse);

                Console.WriteLine($"-----REQUEST-----");
                Console.WriteLine(httpRequest);
                Console.WriteLine($"-----RESPONSE-----");
                Console.WriteLine(httpResponse is FileResponse ? "[[File response truncated]]" : httpResponse.ToString());
                Console.WriteLine();
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);

                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            IHttpResponse httpResponse = null;

            if (httpRequest.Path.Contains("."))
            {
                httpResponse = this.fileHandler.Handle(httpRequest);
            }
            else
            {
                string sessionId = this.SetRequestSession(httpRequest);
                httpResponse = this.mvcRequestHandler.Handle(httpRequest);
                this.SetResponseSession(httpResponse, sessionId);
            }

            return httpResponse;
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.Get(SessionStore.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = SessionStore.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = SessionStore.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse.Headers.Add(
                    new HttpHeader(
                        HttpHeader.SetCookie,
                        $"{SessionStore.SessionCookieKey}={sessionId};HttpOnly=true;Path=/"));
            }
        }

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString()).ToList();
            var byteSegments = new ArraySegment<byte>(responseBytes.ToArray());
            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}