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

            list[0].Notes.Add(new OrderNote("This is a good order!.. NOOOT"));
            list[0].Notes.Add(new OrderNote("This is a very long teeeeeeeeeeeeeeeeeeeeeeeeext text text text text text text text text text text text text..."));
            list[0].Notes.Add(new OrderNote("We are done with station 6"));
            list[0].Notes.Add(new OrderNote("We began with station 7"));
            list[0].Notes.Add(new OrderNote("We began with station 8"));
            list[0].Notes.Add(new OrderNote("We are having trouble with station 7"));
            list[0].Notes.Add(new OrderNote("We need another week to fix the problems with station 7"));
            list[0].Notes.Add(new OrderNote("Station 8:\nGoing good might finish it later today!\nCan help on station 7 by tomorrow."));
            list[0].Notes.Add(new OrderNote("Station 7:\nSounds good!"));

            list[1].StationStatus.Station5 = "Done";
            list[1].StationStatus.Station7 = "Done";
            list[1].StationStatus.Station8 = "Active";

            list[1].Notes.Add(new OrderNote("Small"));
            list[1].Notes.Add(new OrderNote("L\nA\nR\nG\nE because of new lines.. :)"));
            return list; // DBHandler.Instance.
        }
    }
}
