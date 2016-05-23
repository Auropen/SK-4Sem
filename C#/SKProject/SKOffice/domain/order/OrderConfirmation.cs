using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
{
    public class OrderConfirmation
    {
        public string OrderNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string OrderName { get; set; }
        public string Week { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ProducedDate { get; set; }
        public List<string> CompanyInfo { get; private set; }
        public List<string> CustomerInfo { get; private set; }
        public List<string> AltDeliveryInfo { get; private set; }
        public List<string> kitchenInfo { get; private set; }
        public List<string> elementInfo { get; private set; }
        public List<OrderCategory> Categories { get; private set; }


        public OrderConfirmation()
        {
            OrderNumber = "";
            AlternativeNumber = "";
            OrderName = "";
            Week = "";
            OrderDate = new DateTime();
            ProducedDate = new DateTime();
            CompanyInfo = new List<string>();
            CustomerInfo = new List<string>();
            AltDeliveryInfo = new List<string>();
            Categories = new List<OrderCategory>();
            kitchenInfo = new List<string>();
            elementInfo = new List<string>();
        }

        public OrderCategory findCategoryByID(int id)
        {
            foreach (OrderCategory category in Categories)
            {
                if (category.ID == id)
                    return category;
            }
            return null;
        }
    }
}
