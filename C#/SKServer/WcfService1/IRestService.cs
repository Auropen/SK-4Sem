﻿using WcfService.domain.order;
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
        [WebInvoke(Method="GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getAllActiveOrders")]
        List<OrderConfirmation> getAllActiveOrders();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "hasUpdates")]
        int hasUpdates();


        [OperationContract]
        [WebInvoke(Method = "POST", 
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "addOrderConfirmation")]
        string addOrderConfirmation(List<string> fileContent);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "addNote",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        bool addNote(Stream stream);
    }
}
