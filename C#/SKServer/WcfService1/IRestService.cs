using SKOffice.domain.order;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestService" in both code and config file together.
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getOrder/{fileName}")]
        OrderConfirmation getOrder(string fileName);
        

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json ,UriTemplate = "addOrderConfirmation")]
        bool addOrderConfirmation(List<string> fileContent);

    }
}
