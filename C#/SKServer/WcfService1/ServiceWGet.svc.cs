using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Hosting;
using WcfService.domain.data;
using WcfService.domain.order;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceWGet" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceWGet.svc or ServiceWGet.svc.cs at the Solution Explorer and start debugging.
    public class ServiceWGet : IServiceWGet
    {
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
            list[1].StationStatus.Station5 = "Done";
            list[1].StationStatus.Station7 = "Done";
            list[1].StationStatus.Station8 = "Active";
            return list; // DBHandler.Instance.
        }
    }
}
