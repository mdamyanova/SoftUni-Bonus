namespace SoftUni.WebServer.Server
{
    using Handlers;
    using Interfaces;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class HttpServer : IRunnable
    {
        private const string LocalhostIPAddress = "127.0.0.1";

        private readonly int port;
        private readonly IHttpRequestHandler mvcRequestHandler;
        private readonly IHttpRequestHandler fileHandler;
        private readonly TcpListener listener;

        private bool isRunning;

        public HttpServer(int port, IHttpRequestHandler mvcRequestHandler, IHttpRequestHandler fileHandler)
        {
            this.port = port;
            this.listener = new TcpListener(IPAddress.Parse(LocalhostIPAddress), port);

            this.mvcRequestHandler = mvcRequestHandler;
            this.fileHandler = fileHandler;
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at http://{LocalhostIPAddress}:{port}");

            var task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        public async Task ListenLoop()
        {
            while (this.isRunning)
            {
                var client = await this.listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.mvcRequestHandler, this.fileHandler);

               await connectionHandler.ProcessRequestAsync();
            }
        }
    }
}