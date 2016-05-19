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
        public string CompanyInfo { get; set; }
        public string CustomerInfo { get; set; }
        public string AltDeliveryInfo { get; set; }
        public List<OrderElement> Elements { get; private set; }


    }
}
