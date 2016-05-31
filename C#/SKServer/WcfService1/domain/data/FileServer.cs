using SKOffice.domain.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WcfService.domain.data
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
            if (!MainThread.IsAlive)
            {
                Port = port;
                stop = false;
                MainThread.Start();
            }
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
                    Console.WriteLine("Listening on files.");
                    // Thread is suspended while waiting for an incoming connection.
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
                            // Prebuffer, for meta data.
                            var prebuffer = new byte[128];
                            ClientSocket.Receive(prebuffer, prebuffer.Length, SocketFlags.None);
                            string[] metaData = Encoding.UTF8.GetString(prebuffer).Split(';');

                            using (var output = File.Create(metaData[1]))
                            {
                                Console.WriteLine("Client connected. Starting to receive " + metaData[1] + ", size: " + metaData[2] + ", file type: " + metaData[0]);

                                // Buffer, the file data
                                var buffer = new byte[ClientSocket.ReceiveBufferSize];
                                int totalSize = 0;
                                // Starts writing from the buffer to the file
                                while ((bytesRead = ClientSocket.Receive(buffer, buffer.Length, SocketFlags.None)) > 0)
                                {
                                    output.Write(buffer, 0, bytesRead);
                                    Console.WriteLine("Reading file: " + (totalSize =+ bytesRead) + "/" + metaData[2]);
                                }

                                Console.WriteLine("Successfully read the file, cleaning and closing file stream...");
                                output.Flush();
                                output.Close();
                                Console.WriteLine("Stream has been cleaned and closed.");
                            }

                            // Return a success msg.
                            Console.WriteLine("Sending success msg back to the client.");
                            byte[] msg = Encoding.UTF8.GetBytes("200;OK;File was uploaded");
                            ClientSocket.Send(msg, msg.Length, SocketFlags.None);
                            Console.WriteLine("Client received success msg.");
                        }
                    }
                    catch (Exception ex)
                    {
                        /*byte[] msg = Encoding.UTF8.GetBytes("400;OK;Upload was interrupted");
                        ClientSocket.Send(msg, msg.Length, SocketFlags.None);*/
                        Console.WriteLine("Something went wrong.. " + ex.Message);
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