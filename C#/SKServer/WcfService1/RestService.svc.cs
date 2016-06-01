using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SKOffice.domain.order;
using SKOffice;
using WcfService.domain.data;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Net;
using System.Web.Hosting;
using System.ServiceModel.Web;

namespace WcfService
{
    public class RestService : IRestService
    {
        OrderConfirmation IRestService.getOrder(string fileName)
        {
            Console.WriteLine("Got RESTFul connection.");
            return OrderParser.Instance.readOrder("C:\\School\\SKøkken\\" + fileName + ".e02");
        }

        public bool addOrderConfirmation(List<string> fileContent)
        {
            try
            {
                string fileName = "testfile";
                string fileExtension = ".e02";
                FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/FileServer/" + fileName + "/"), fileName + "." + fileExtension));

                fileInfo.Directory.Create();
                using (var output = new StreamWriter(File.Create(fileInfo.FullName)))
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
    }
}
