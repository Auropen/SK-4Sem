using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.domain.utility
{
    public class ConversionUtil
    {
        private ConversionUtil()
        {

        }

        public static int stringToInt(string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (FormatException ex)
            {
                Console.Error.WriteLine("Error: Couldn't convert " + s + " to int./n" + ex.Message);
            }
            return Int32.MinValue;
        }
    }
}
