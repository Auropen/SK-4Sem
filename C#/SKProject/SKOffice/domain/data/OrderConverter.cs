using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKOffice.domain.order;
using SKOffice.domain.utility;

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
                        string[] date = lineSplit[4].Split('/');
                        string[] time = lineSplit[5].Split(':');

                        DateTime dateTime = new DateTime(
                            ConversionUtil.stringToInt(date[2]), 
                            ConversionUtil.stringToInt(date[1]), 
                            ConversionUtil.stringToInt(date[0]),
                            ConversionUtil.stringToInt(time[0]),
                            ConversionUtil.stringToInt(time[1]),
                            ConversionUtil.stringToInt(time[2]));

                        result.Date = dateTime;
                        break;
                    case "101": // Company Info
                        result.CompanyInfo.Add(lineSplit[1]);
                        result.CompanyInfo.Add(lineSplit[3]);
                        result.CompanyInfo.Add(lineSplit[4]);
                        result.CompanyInfo.Add(lineSplit[5]);
                        result.CompanyInfo.Add(lineSplit[20]);
                        result.CompanyInfo.Add(lineSplit[24]);
                        result.CompanyInfo.Add(lineSplit[25]);
                        result.CompanyInfo.Add(lineSplit[30]);

                        break;
                    case "211": // Customer Info
                        result.CustomerInfo.Add(lineSplit[1]);
                        result.CustomerInfo.Add(lineSplit[3]);
                        result.CustomerInfo.Add(lineSplit[4]);
                        result.CustomerInfo.Add(lineSplit[5]);
                        result.CustomerInfo.Add(lineSplit[20]);
                        result.CustomerInfo.Add(lineSplit[24]);
                        result.CustomerInfo.Add(lineSplit[25]);
                        break;
                    case "212": // Customers Alternative Delivery Address
                        result.AltDeliveryInfo.Add(lineSplit[1]);
                        result.AltDeliveryInfo.Add(lineSplit[3]);
                        result.AltDeliveryInfo.Add(lineSplit[4]);
                        result.AltDeliveryInfo.Add(lineSplit[5]);
                        result.AltDeliveryInfo.Add(lineSplit[11]);
                        result.AltDeliveryInfo.Add(lineSplit[19]);
                        result.AltDeliveryInfo.Add(lineSplit[20]);
                        result.AltDeliveryInfo.Add(lineSplit[24]);
                        result.AltDeliveryInfo.Add(lineSplit[25]);
                        result.AltDeliveryInfo.Add(lineSplit[26]);
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
