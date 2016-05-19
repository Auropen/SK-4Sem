using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKOffice.domain.order;

namespace SKOffice
{
    public class OrderConverter
    {
        private static OrderConverter instance;

        public static OrderConverter Instance
        {
            get
            {
                if (instance == null) instance = new OrderConverter();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        
        /// <summary>
        /// Singleton constructor. This class can only be created from within.
        /// </summary>
        private OrderConverter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public OrderConfirmation readOrder(string filePath)
        {
            if (!filePath.ToLower().EndsWith(".e02"))
            {
                Console.Error.WriteLine("Error: Wrong file type.");
                return null;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("Error: File doesn't exist.");
                return null;
            }
            

            StreamReader stream = new StreamReader(filePath);
            OrderConfirmation result = new OrderConfirmation();
            
            while (stream.Peek() > -1)
            {
                string line = stream.ReadLine();
                string[] lineSplit = line.Split(';');
                

                if (lineSplit.Length == 0)
                    continue;


                switch (lineSplit[0])
                {
                    case "0": // Program header + creation date

                        break;
                    case "100": // Company Info
                        break;
                    case "200": // Customer Info
                        break;
                    case "300": // Alternative Nr
                        break;
                    case "400": // Order Info
                        break;
                    case "430": // Order Container Info
                        break;
                    case "500": // Order Block
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
