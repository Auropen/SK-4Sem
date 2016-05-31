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
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we&rsquo;re doing a POST
            req.ContentType = "application / x - www - form - urlencoded";
            req.Method = "POST";
            //req.Timeout = 200000;

            /*This is optional*/

            FileInfo finfo = new FileInfo(filePath);
            string fileName = finfo.Name;

            (req as HttpWebRequest).Referer = "http://" + ipEndPoint.Address + ":" + ipEndPoint.Port + "/RestService.svc/upload/" + fileName;
            (req as HttpWebRequest).UserAgent = @"Mozilla / 5.0(Windows NT 6.2; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 29.0.1547.76 Safari / 537.36 & Prime";
            (req as HttpWebRequest).Headers.Add("Origin", "http://" + ipEndPoint.Address + ":" + ipEndPoint.Port + "/RestService.svc/upload/" + fileName);

            /*This is optional*/

            //We need to count how many bytes we're sending. Posted Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp;
            resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public void sendFile(List<string> filePaths)
        {
            try
            {
                /*if (!client.Connected)
                    client.Connect(ipEndPoint);*/
                foreach (string path in filePaths)
                {
                    using (WebClient client = new WebClient())
                    {
                        // Fetches file info
                        FileInfo finfo = new FileInfo(path);
                        string fileType = finfo.Name.Split('.')[1];
                        string fileName = finfo.Name;
                        string fileSize = finfo.Length + "";                                       //RestService.svc 
                        client.UploadFile("http://" + ipEndPoint.Address + ":" + ipEndPoint.Port + "/RestService.svc/upload/" + fileName, "POST", path);
                    }
                }
            }
            catch (Exception)
            {

            }





                    /*try
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
                            long timeStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            string msg = "";
                            Console.WriteLine("Waiting on response " + path);
                            while (!(msg.Contains("200")))
                            {
                                int size = 0;
                                if (client.Available > 0)
                                {
                                    byte[] buffer = new byte[client.SendBufferSize];
                                    Console.WriteLine("Got something.. [" + client.Available + "]");
                                    size = client.Receive(buffer, buffer.Length, SocketFlags.None);
                                    Console.WriteLine("Read the buffer, " + size);
                                    msg += Encoding.UTF8.GetString(buffer);
                                    Console.WriteLine("Got message: " + msg);
                                }

                                //Stops waiting after 5 seconds
                                if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - timeStart > 5000)
                                {
                                    Console.WriteLine("Got no response, stopping file transfer.");
                                    goto outOfLoop;
                                }
                                Thread.Sleep(SHORT_DELAY);
                            }
                            Console.WriteLine("Got " + msg);
                        }
                    outOfLoop:;
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        client.Close();
                    }*/
                }
    }
}
