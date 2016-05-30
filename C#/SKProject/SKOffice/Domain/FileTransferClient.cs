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
            byte[] ipAddress = { 10, 176, 164, 144 };
            ipEndPoint = new IPEndPoint(new IPAddress(ipAddress),9000);

            // Create a TCP socket.
            client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        public void sendFile(List<string> filePaths)
        {

            try
            {
                client.Connect(ipEndPoint);
                foreach (string path in filePaths)
                {
                    // Fetches file info
                    FileInfo finfo = new FileInfo(path);
                    string fileType = finfo.Name.Split('.')[1];
                    string fileName = finfo.Name;
                    string fileSize = finfo.Length + "";

                    // Creates pre and post buffer
                    byte[] preBuffer = new byte[128];
                    preBuffer = Encoding.UTF8.GetBytes(String.Format("{0};{1};{2}", fileType, fileName, fileSize));
                    byte[] postBuffer = new byte[0];

                    // Sends file
                    client.SendFile(path, preBuffer, postBuffer, TransmitFileOptions.UseDefaultWorkerThread);
                }
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
