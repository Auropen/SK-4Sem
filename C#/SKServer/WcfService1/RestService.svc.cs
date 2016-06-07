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
            list[0].StationStatus.Station4 = "Active";
            list[0].StationStatus.Station6 = "Done";
            list[0].StationStatus.Station7 = "Active";

            list[0].Notes.Add(new OrderNote("This is a good order!.. NOOOT"));
            list[0].Notes.Add(new OrderNote("This is a very long teeeeeeeeeeeeeeeeeeeeeeeeext text text text text text text text text text text text text..."));

            list[1].StationStatus.Station5 = "Done";
            list[1].StationStatus.Station7 = "Done";
            list[1].StationStatus.Station8 = "Active";

            list[1].Notes.Add(new OrderNote("Small"));
            list[1].Notes.Add(new OrderNote("L\nA\nR\nG\nE because of new lines.. :)"));
            return list; // DBHandler.Instance.
        }

        public bool addNote(string dataText)
        {
            string[] data = dataText.Split("%ENDMETA%".ToArray());
            if (data.Length != 2)
                return false;

            try
            {
                DBHandler.Instance.createNotes(data[0], data[1]);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
