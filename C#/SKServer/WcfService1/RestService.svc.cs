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
            string fileExtension = ".e02";
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/FileServer/" + fileName + "/"), fileName + "." + fileExtension));
            return OrderParser.Instance.readOrder(fileInfo.FullName);
        }

        public bool addOrderConfirmation(List<string> fileContent)
        {
            try
            {
                string fileName = "testfile";
                string fileExtension = ".e02";
                FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/FileServer/" + fileName + "/"), fileName + "." + fileExtension));

                fileInfo.Directory.Create();
                using (var output = new StreamWriter(File.Create(fileInfo.FullName), Encoding.Default))
                {
                    foreach (string line in fileContent)
                    {
                        output.WriteLine(line);
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
            return null; // DBHandler.Instance.
        }

        public bool addNote(string text, string orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
