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
            clients = new List<Client>();
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
                    Console.WriteLine("Listening on files.");
                    // Thread is suspended while waiting for an incoming connection.
                    Client client = new Client(listener.Accept());
                    clients.Add(client);
                    client.startClient();
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
                Console.WriteLine("Client connected from " + ClientSocket.LocalEndPoint);

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
                    FileStream output = null;
                    try
                    {
                        int bytesRead;
                        //Prebuffer, for 128byte meta data.
                        var prebuffer = new byte[128];
                        ClientSocket.Receive(prebuffer, prebuffer.Length, SocketFlags.None);
                        string[] metaData = Encoding.UTF8.GetString(prebuffer).Split(';');

                        if (metaData.Length < 2)
                            break;

                        output = File.Create(metaData[1]);

                        Console.WriteLine("Client connected. Starting to receive " + metaData[1] + ", size: " + metaData[2] + ", file type: " + metaData[0]);

                        var buffer = new byte[ClientSocket.ReceiveBufferSize];
                        int totalSize = 0;
                        //Buffer, for file data

                        if (Convert.ToInt32(metaData[2]) >= totalSize && ClientSocket.Available > 0)
                        {
                            while ((bytesRead = ClientSocket.Receive(buffer, buffer.Length, SocketFlags.None)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                                Console.WriteLine("Reading file: " + (totalSize += bytesRead) + "/" + metaData[2]);
                            }
                        }

                        // Return a success msg
                        Console.WriteLine("Sending success msg back to the client.");
                        byte[] msg = Encoding.UTF8.GetBytes("200;OK;File was uploaded");
                        ClientSocket.Send(msg, msg.Length, SocketFlags.None);

                        //Closing up file stream
                        Console.WriteLine("Successfully read the file, cleaning and closing file stream...");
                        output.Flush();
                        output.Close();
                        output = null;
                        Console.WriteLine("Stream has been cleaned and closed.");
                    }
                    catch (SocketException socketEx)
                    {
                        Console.WriteLine("Connection lost.. " + socketEx.Message);
                        Console.WriteLine(socketEx.StackTrace);
                        stop = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something went wrong.. " + ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        stop = true;
                        break;
                    }
                    finally
                    {
                        if (output != null)
                        {
                            Console.WriteLine("Cleaning and closing file stream...");
                            output.Flush();
                            output.Close();
                            Console.WriteLine("Stream has been cleaned and closed.");
                        }
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