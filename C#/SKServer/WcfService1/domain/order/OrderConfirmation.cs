using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WcfService.domain.order
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
        public string Status { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public DateTime ProducedDate { get; set; }
        [DataMember]
        public string HousingAssociation { get; set; }
        [DataMember]
        public List<string> CompanyInfo { get; private set; }
        [DataMember]
        public List<string> DeliveryInfo { get; private set; }
        [DataMember]
        public List<string> AltDeliveryInfo { get; private set; }
        [DataMember]
        public List<string> KitchenInfo { get; private set; }
        [DataMember]
        public List<OrderCategory> Categories { get; private set; }
        [DataMember]
        public List<OrderNote> Notes { get; private set; }
        [DataMember]
        public OrderStatus StationStatus { get; private set; }


        public OrderConfirmation()
        {
            OrderNumber = "";
            AlternativeNumber = "";
            OrderName = "";
            Week = "";
            Status = "";
            HousingAssociation = "";
            OrderDate = new DateTime();
            ProducedDate = new DateTime();
            CompanyInfo = new List<string>();
            DeliveryInfo = new List<string>();
            AltDeliveryInfo = new List<string>();
            Categories = new List<OrderCategory>();
            Notes = new List<OrderNote>();
            KitchenInfo = new List<string>();
            StationStatus = new OrderStatus();
        }

        public OrderCategory findCategoryByParserID(int parserID)
        {
            foreach (OrderCategory category in Categories)
            {
                if (category.ParserID == parserID)
                    return category;
            }
            return null;
        }
    }
}
