using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
   public  class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener listerner;

        private readonly ServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, ServerRoutingTable serverRoutingTable)
        {
            this.port = port;
            this.listerner = new TcpListener(IPAddress.Parse(LocalHostIpAddress), this.port);
            this.serverRoutingTable = serverRoutingTable;
        }

        public void Run()
        {
            this.listerner.Start();
            this.isRunning = true;

            Console.WriteLine($"Server is running on http://${LocalHostIpAddress}:{this.port}");

            Task task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        public async Task ListenLoop()
        {
            while (this.isRunning)
            {
                var client = await this.listerner.AcceptSocketAsync();
                var connectionHeader = new ConnectionHandler(client, serverRoutingTable);
                var responseTask = connectionHeader.ProcessRequestAsync();

                responseTask.Wait();
            }
        }
    }
}
