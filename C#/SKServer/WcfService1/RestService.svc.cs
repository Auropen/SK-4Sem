﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SKOffice.domain.order;
using SKOffice;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestService.svc or RestService.svc.cs at the Solution Explorer and start debugging.
    public class RestService : IRestService
    {
        OrderConfirmation IRestService.json(string fileName)
        {
            Console.WriteLine("Got RESTFul connection.");
            return OrderParser.Instance.readOrder("C:\\School\\SKøkken\\" + fileName + ".e02");
        }
    }
}