using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain
{

    public class FileTransferClient
    {


        private static FileTransferClient instance;

        public static FileTransferClient Instance
        {
            get
            {
                if (instance == null) instance = new FileTransferClient();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public IPHostEntry ipHost { get; private set; }
        public IPAddress ipAddr { get; private set; }
        public IPEndPoint ipEndPoint { get; private set; }
        public Socket client { get; private set; }

        private FileTransferClient()
        {
            byte[] ipAddress = { 10, 176, 164, 98 };
            ipEndPoint = new IPEndPoint(new IPAddress(ipAddress),9000);

            // Create a TCP socket.
            client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        public void sendFile(List<string> filePaths)
        {

            try
            {
                if (!client.Connected)
                    client.Connect(ipEndPoint);
                foreach (string path in filePaths)
                {
                    
                    // Fetches file info
                    FileInfo finfo = new FileInfo(path);
                    string fileType = finfo.Name.Split('.')[1];
                    string fileName = finfo.Name;
                    string fileSize = finfo.Length + "";

                    // Creates pre and post buffer
                    byte[] preBufferFilling = Encoding.UTF8.GetBytes(String.Format("{0};{1};{2}", fileType, fileName, fileSize));
                    byte[] preBuffer = new byte[128];
                    Array.Copy(preBufferFilling, preBuffer, preBufferFilling.Length);
                    for (int i = preBufferFilling.Length; i < preBuffer.Length; i++)
                        preBuffer[i] = Convert.ToByte(';');
                    byte[] postBuffer = new byte[0];

                    Console.WriteLine("Sending metadata for " + path + ", containing: " + Encoding.UTF8.GetString(preBuffer));
                    // Sends file
                    Console.WriteLine("Sending file " + path);
                    client.SendFile(path, preBuffer, postBuffer, TransmitFileOptions.UseDefaultWorkerThread);

                    //Waiting on respond
                    long timeStart = DateTime.Now.Millisecond;
                    string msg = "";
                    Console.WriteLine("Waiting on response " + path);
                    while (!(msg.Length > 0))
                    {
                        byte[] buffer = new byte[client.SendBufferSize];
                        while ((client.Receive(buffer, buffer.Length, SocketFlags.None)) > 0)
                        {
                            msg = Encoding.UTF8.GetString(buffer);
                        }

                        if (DateTime.Now.Millisecond - timeStart > 5000)
                        {
                            Console.WriteLine("File was not send.");
                            goto outOfLoop;
                        }
                    }
                    Console.WriteLine("Got 200-OK");
                }
                outOfLoop:;
            }
            catch (Exception)
            {
            }
            finally
            {
                client.Close();
            }
        }
    }
}
