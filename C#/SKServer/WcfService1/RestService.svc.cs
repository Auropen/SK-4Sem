using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.domain.order;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Net;
using System.Web.Hosting;
using System.ServiceModel.Web;
using WcfService.domain.data;
using WcfService.technical;

namespace WcfService
{
    public class RestService : IRestService
    {
        public static int Updates = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        OrderConfirmation IRestService.getOrder(string fileName)
        {
            string fileExtension = "e02";
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/" + fileName + "/"), fileName + "." + fileExtension));
            return OrderParser.Instance.readOrder(fileInfo.FullName);
        }

        /// <summary>
        /// Checks if the file exists or creates it on to the Database 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public string addOrderConfirmation(List<string> fileContent)
        {
            try
            {
                string fileName = fileContent[0].Split('.')[0];
                string fileExtension = fileContent[0].Split('.')[1];
                FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/" + fileName + "/"), fileName + "." + fileExtension));

                if (fileInfo.Exists)
                    return "ERROR: File already exists.";

                fileInfo.Directory.Create();
                using (var output = new StreamWriter(File.Create(fileInfo.FullName), Encoding.Default))
                {
                    for (int i = 1; i < fileContent.Count; i++)
                    {
                        output.WriteLine(fileContent[i]);
                    }
                    output.Flush();
                    output.Close();
                }

                OrderConfirmation oc = OrderParser.Instance.readOrder(fileInfo.FullName);

                DBHandler.Instance.createOrder(oc);
            }
            catch (IOException)
            {
                return "ERROR: File was not uploaded correctly.";
            }
            Updates++;
            return "OK: File was successfully uploaded to the service.";
        }

        /// <summary>
        /// Returns all the active orders from the Database as a list
        /// </summary>
        /// <returns></returns>
        public List<OrderConfirmation> getAllActiveOrders()
        {
            return DBHandler.Instance.getAllOrdersOfStatus("Active");
        }

        public int getUpdates()
        {
            return Updates;
        }

        /// <summary>
        /// Saves the note from the Android and stores it in the Database
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool addNote(Stream stream)
        {
            string dataText = "";
            try
            {
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);

                dataText = sr.ReadToEnd().Replace("\"", "");
                int index = dataText.IndexOf(';');
                string orderNumber = dataText.Substring(0, index).Trim();
                string value = dataText.Substring(index + 1).Trim();

                DBHandler.Instance.createNotes(orderNumber, new OrderNote(value));
            }
            catch (Exception)
            {
                return false;
            }
            Updates++;
            return true;
        }
    }
}
