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
                while (!stop)
                {
                    try
                    {
                        if (ClientSocket.Available > 0)
                        {
                            int bytesRead;
                            //Prebuffer, for meta data.
                            var prebuffer = new byte[128];
                            ClientSocket.Receive(prebuffer, prebuffer.Length, SocketFlags.None);
                            string[] metaData = Encoding.UTF8.GetString(prebuffer).Split(';');

                            using (var output = File.Create("result.dat"))
                            {
                                Console.WriteLine("Client connected. Starting to receive the file");

                                var buffer = new byte[ClientSocket.ReceiveBufferSize];
                                //Buffer, the file data
                                while ((bytesRead = ClientSocket.Receive(buffer, buffer.Length, SocketFlags.None)) > 0)
                                    output.Write(buffer, 0, bytesRead);
                            }

                            // Return a success msg.
                            byte[] msg = Encoding.UTF8.GetBytes("200;OK;File was uploaded");
                            ClientSocket.Send(msg, msg.Length, SocketFlags.None);
                        }
                    }
                    catch (Exception)
                    {
                        byte[] msg = Encoding.UTF8.GetBytes("400;OK;Upload was interrupted");
                        ClientSocket.Send(msg, msg.Length, SocketFlags.None);
                    }
                    
                    Thread.Sleep(SHORT_DELAY);
                }
            }

            public void stopClient()
            {
                stop = true;
            }
        }
    }
}