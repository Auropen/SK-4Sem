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
            Properties prop = new Properties("config/connection.ini");
            prop.set("ip", "10.176.164.183");
            prop.set("port", "9000");
            prop.Save();
            string ip = Properties.Settings.Default.ip;
            string port = Properties.Settings.Default.port;
            Console.WriteLine(ip + ":" + port);
            
        }
    }
}
