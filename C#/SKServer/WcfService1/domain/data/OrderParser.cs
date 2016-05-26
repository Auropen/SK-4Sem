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
    public class OrderParser
    {
        private static OrderParser instance;

        public static OrderParser Instance
        {
            get
            {
                if (instance == null) instance = new OrderParser();
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
        private OrderParser()
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
            

            StreamReader stream = new StreamReader(filePath, Encoding.Default);
            OrderConfirmation result = new OrderConfirmation();

            int kitchenInfoNumber = 0;
            OrderElement lastElement = null;

            while (stream.Peek() > -1)
            {
                string line = stream.ReadLine();
                string[] lineSplit = line.Split(';');
                string[] date;

                if (lineSplit.Length == 0)
                    continue;


                switch (lineSplit[0])
                {
                    case "0": // Program header + produced date
                        date = lineSplit[4].Split('/');
                        string[] time = lineSplit[5].Split(':');

                        DateTime dateTime = new DateTime(
                            ConversionUtil.stringToInt(date[2]), 
                            ConversionUtil.stringToInt(date[1]), 
                            ConversionUtil.stringToInt(date[0]),
                            ConversionUtil.stringToInt(time[0]),
                            ConversionUtil.stringToInt(time[1]),
                            ConversionUtil.stringToInt(time[2]));

                        result.ProducedDate = dateTime;
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
                    case "300": // Rightside Info
                        result.OrderNumber = lineSplit[1];
                        result.OrderName = lineSplit[3];
                        date = lineSplit[4].Split('/');
                        result.OrderDate = new DateTime(
                            ConversionUtil.stringToInt(date[2]),
                            ConversionUtil.stringToInt(date[1]),
                            ConversionUtil.stringToInt(date[0]),12,0,0);
                        result.Week = lineSplit[7];
                        
                        string[] splitName = result.OrderName.Split('/');
                        result.AlternativeNumber =
                            splitName[0] + " " +
                            result.OrderNumber + " " +
                            splitName[1];
                        break;
                    case "410": // Start of kitchen info
                        result.kitchenInfo.Add("KInfo-" + kitchenInfoNumber);
                        kitchenInfoNumber++;
                        break;
                    case "423": // Title of the order part
                        result.kitchenInfo.Add(lineSplit[2]);
                        break;
                    case "424": // Text for the order part (423)
                        result.kitchenInfo.Add(lineSplit[2]);
                        break;
                    case "425": // Finish of the order part
                        result.kitchenInfo.Add(lineSplit[2]); // Title
                        result.kitchenInfo.Add(lineSplit[4]); // Description
                        break;
                    case "430": // Order Category Info
                        result.Categories.Add(new OrderCategory(lineSplit[2], ConversionUtil.stringToInt(lineSplit[1])));
                        break;
                    case "500": // Order Element
                        lastElement = new OrderElement(lineSplit[1],"", "", lineSplit[9], lineSplit[10]);

                        int metaPos = ConversionUtil.stringToInt(lineSplit[2]);
                        if (metaPos > 0)
                            lastElement.Position += "." + metaPos;

                        lastElement.ElementInfo.Add(lineSplit[3]);
                        lastElement.ElementInfo.Add(lineSplit[8]);
                        result.findCategoryByID(ConversionUtil.stringToInt(lineSplit[12])).Elements.Add(lastElement);
                        break;
                    case "501": // Order Element
                        Console.WriteLine(line);
                        lastElement.Hinge = lineSplit[8];
                        break;
                    case "502": // Order Element
                        lastElement.ElementInfo.Add(lineSplit[1]);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
