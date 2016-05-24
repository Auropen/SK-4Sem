using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.domain.Connection
{
    public class Server
    {
        private static Server instance;
        private Boolean stop;
        public Thread MainThread { get; private set; }
        public int Port { get; private set; }
        public TcpListener listener { get; private set; }
        public List<TcpClient> clients { get; private set; }
        public static Server Instance
        {
            get
            {
                if (instance == null) instance = new Server();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private Server()
        {
            
        }

        public void startServer(int port)
        {
            Port = port;
            stop = false;

            MainThread = new Thread(new ThreadStart(threadedLoop));
            MainThread.Start();
        }

        private void threadedLoop()
        {
            listener = new TcpListener(IPAddress.Any,Port);
            listener.Start();
            while (!stop)
            {
                TcpClient client = listener.AcceptTcpClient();
                
            }
        }

        public void stopServer()
        {
            stop = true;
        }
    }

    public class Client
    {
        public TcpClient TheClient { get; set; }

        public Client(TcpClient client)
        {
            TheClient = client;
            Console.WriteLine("Client connected from " + TheClient.Client.AddressFamily);
        }
    }
}
