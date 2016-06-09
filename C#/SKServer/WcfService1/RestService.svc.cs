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
        private static int updates = 0;

        OrderConfirmation IRestService.getOrder(string fileName)
        {
            string fileExtension = "e02";
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/" + fileName + "/"), fileName + "." + fileExtension));
            return OrderParser.Instance.readOrder(fileInfo.FullName);
        }

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
            return "OK: File was successfully uploaded to the service.";
        }

        public List<OrderConfirmation> getAllActiveOrders()
        {
            return DBHandler.Instance.getAllOrdersOfStatus("Active");
        }

        public int hasUpdates()
        {
            return updates;
        }

        public bool addNote(Stream stream)
        {
            string dataText = "";
            try
            {
                StreamReader sr = new StreamReader(stream);

                dataText = sr.ReadToEnd();

                string[] data = dataText.Split("%ENDMETA%".ToArray());
                if (data.Length != 2)
                    return false;

                DBHandler.Instance.createNotes(data[0], new OrderNote(data[1]));
            }
            catch (Exception)
            {
                return false;
            }
            updates++;
            return true;
        }
    }
}
