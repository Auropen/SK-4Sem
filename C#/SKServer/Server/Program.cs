using Server.domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 0;
            SKServer s = new SKServer();
            while (true)
            {
                Console.WriteLine("Testing: " + i++);
                Thread.Sleep(5000);
            }
        }





    }
}
