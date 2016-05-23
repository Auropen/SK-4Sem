using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.domain
{
    class SKServer
    {
         
        public SKServer()
        {
            string ip = ConfigurationManager.AppSettings["ip"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            Console.WriteLine(ip + ":" + port);
        }
    }
}
