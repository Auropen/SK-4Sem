using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
{
    public class OrderConfirmation
    {
        public DateTime Date { get; set; }
        public List<string> CompanyInfo { get; set; }
        public List<string> CustomerInfo { get; set; }
        public List<string> AltDeliveryInfo { get; set; }
        public List<OrderElement> Elements { get; private set; }

        public OrderConfirmation()
        {
            Date = new DateTime();
            CompanyInfo = new List<string>();
            CustomerInfo = new List<string>();
            AltDeliveryInfo = new List<string>();
            Elements = new List<OrderElement>();
        }
    }
}
