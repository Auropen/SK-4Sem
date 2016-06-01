using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SKOffice.domain.order
{
    [DataContract]
    public class OrderConfirmation
    {
        [DataMember]
        public string OrderNumber { get; set; }
        [DataMember]
        public string AlternativeNumber { get; set; }
        [DataMember]
        public string OrderName { get; set; }
        [DataMember]
        public string Week { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public DateTime ProducedDate { get; set; }
        [DataMember]
        public List<string> CompanyInfo { get; private set; }
        [DataMember]
        public List<string> CustomerInfo { get; private set; }
        [DataMember]
        public List<string> AltDeliveryInfo { get; private set; }
        [DataMember]
        public List<string> kitchenInfo { get; private set; }
        [DataMember]
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
