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
        OrderConfirmation IRestService.getOrder(string fileName)
        {
            string fileExtension = "e02";
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/" + fileName + "/"), fileName + "." + fileExtension));
            return OrderParser.Instance.readOrder(fileInfo.FullName);
        }

        public bool addOrderConfirmation(List<string> fileContent)
        {
            try
            {
                string fileName = fileContent[0].Split('.')[0];
                string fileExtension = fileContent[0].Split('.')[1];
                FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/" + fileName + "/"), fileName + "." + fileExtension));

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

            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public List<OrderConfirmation> getAllActiveOrders()
        {
            List<OrderConfirmation> list = new List<OrderConfirmation>();
            string fileExtension = "e02";
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/w0000520/"), "w0000520." + fileExtension));
            list.Add(OrderParser.Instance.readOrder(fileInfo.FullName));
            fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Order/w0000524/"), "w0000524." + fileExtension));
            list.Add(OrderParser.Instance.readOrder(fileInfo.FullName));
            return list; // DBHandler.Instance.
        }

        public bool addNote(string dataText)
        {
            try
            {

                DBHandler.Instance.createNotes("",0);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
