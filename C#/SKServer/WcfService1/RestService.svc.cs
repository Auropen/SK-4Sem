using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SKOffice.domain.order;
using SKOffice;
using WcfService.domain.data;

namespace WcfService
{
    public class RestService : IRestService
    {
        public RestService()
        {
            FileServer fs = FileServer.Instance;
            fs.startServer(9000);
        }

        OrderConfirmation IRestService.getOrder(string fileName)
        {
            Console.WriteLine("Got RESTFul connection.");
            return OrderParser.Instance.readOrder("C:\\School\\SKøkken\\" + fileName + ".e02");
        }
    }
}
