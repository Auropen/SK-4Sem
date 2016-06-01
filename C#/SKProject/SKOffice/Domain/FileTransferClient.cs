using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        //public Socket client { get; private set; }
        private const int SHORT_DELAY = 5;

        private FileTransferClient()
        {
            byte[] ipAddress = { 10, 176, 164, 98 };
            ipEndPoint = new IPEndPoint(new IPAddress(ipAddress),8080);

            // Create a TCP socket.
            /*client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);*/
        }

        public string HttpPostFiles(string URI, string Parameters, string filePath)
        {
            RestService.RestServiceClient rsclient = new RestService.RestServiceClient();

            rsclient.addOrderConfirmation();
        }
    }
}
