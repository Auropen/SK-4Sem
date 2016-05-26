using SKOffice.domain.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WcfService1.domain.data
{
    public class FileServer
    {
        /// <summary>
        /// Used at loops to avoid busy loops
        /// </summary>
        public const int SHORT_DELAY = 5;
        private static FileServer instance;
        private Boolean stop;
        public Thread MainThread { get; private set; }
        public int Port { get; private set; }
        public Socket listener { get; private set; }
        public List<Client> clients { get; private set; }
        public static FileServer Instance
        {
            get
            {
                if (instance == null) instance = new FileServer();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private FileServer()
        {
            MainThread = new Thread(new ThreadStart(threadedLoop));
        }

        public void startServer(int port)
        {
            Port = port;
            stop = false;
            MainThread.Start();
        }

        private void threadedLoop()
        {
            try
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Any, Port));
                listener.Listen(10);
                while (!stop)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    clients.Add(new Client(listener.Accept()));
                    Thread.Sleep(SHORT_DELAY);
                }
            }
            catch (SocketException)
            {
            }
            
        }

        public void stopServer()
        {
            stop = true;
        }

        /// <summary>
        /// Going to be the instance of the client connected to the server.
        /// </summary>
        public class Client
        {
            public Socket ClientSocket { get; private set; }
            public Thread Thread { get; private set; }
            private bool stop;

            public Client(Socket socket)
            {
                stop = true;
                ClientSocket = socket;
                Console.WriteLine("Client connected from " + ClientSocket.LocalEndPoint.AddressFamily);

                Thread = new Thread(new ThreadStart(listener));
            }

            public void startClient()
            {
                stop = false;
                Thread.Start();
            }

            private void listener()
            {
                //Socket.SendFile();
                //ClientSocket.Receive()
                /*while (!stop)
                {
                    if (stream.Peek() > -1)
                    {
                        string filename = stream.ReadLine();
                        int filesize = ConversionUtil.stringToInt(stream.ReadLine());
                        byte[] data = stream.rea
                    }
                    Thread.Sleep(SHORT_DELAY);
                }*/
            }

            public void stopClient()
            {
                stop = true;
            }
        }
    }
}