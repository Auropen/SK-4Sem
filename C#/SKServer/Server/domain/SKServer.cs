using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.domain
{
    class SKServer
    {
         
        public SKServer()
        {
            Properties prop = new Properties("connection.ini");
            prop.set("ip", "10.176.164.183");
            prop.set("port", "9000");
            prop.Save();
            string ip = prop.get("ip");
            string port = prop.get("port");
            Console.WriteLine(ip + ":" + port);
            
        }
    }
}
