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
using WcfService.technical;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceWGet" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceWGet.svc or ServiceWGet.svc.cs at the Solution Explorer and start debugging.
    public class ServiceWGet : IServiceWGet
    {
        public List<OrderConfirmation> getAllActiveOrders()
        {
            return DBHandler.Instance.getAllOrdersOfStatus("Active");
        }
    }
}
