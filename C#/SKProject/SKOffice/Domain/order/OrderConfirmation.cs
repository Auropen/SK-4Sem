using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SKOffice.ServiceReference1;

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

        public OrderCategory findCategoryByID(int id)
        {
            foreach (OrderCategory category in Categories)
            {
                if (category.ID == id)
                    return category;
            }
            return null;
        }

        public static explicit operator OrderConfirmation(SKOffice.ServiceReference1.OrderConfirmation otherOC)
        {
            OrderConfirmation oc = new OrderConfirmation();
            oc.AltDeliveryInfo = convertArrayToList<string>(otherOC.AltDeliveryInfo);
            oc.AlternativeNumber = otherOC.AlternativeNumber;
            foreach (SKOffice.ServiceReference1.OrderCategory otherCategory in otherOC.Categories)
            {
                OrderCategory category = new OrderCategory(otherCategory.Name, otherCategory.ID);

                foreach (SKOffice.ServiceReference1.OrderElement otherElement in otherCategory.Elements)
                {
                    category.Elements.Add(new OrderElement(otherElement.Position, otherElement.Hinge, otherElement.Finish, otherElement.Amount, otherElement.Unit));
                }

                oc.Categories.Add(category);
            }
            oc.CompanyInfo = convertArrayToList<string>(otherOC.CompanyInfo);
            oc.DeliveryInfo = convertArrayToList<string>(otherOC.DeliveryInfo);
            foreach (SKOffice.ServiceReference1.OrderNote otherNote in otherOC.Notes)
            {
                OrderNote note = new OrderNote(otherNote.Text);
                oc.Notes.Add(note);
            }
            oc.OrderDate = otherOC.OrderDate;
            oc.OrderName = otherOC.OrderName;
            oc.OrderNumber = otherOC.OrderNumber;
            oc.ProducedDate = otherOC.ProducedDate;
            OrderStatus status = new OrderStatus(
                otherOC.StationStatus.Station4, 
                otherOC.StationStatus.Station5, 
                otherOC.StationStatus.Station6, 
                otherOC.StationStatus.Station7, 
                otherOC.StationStatus.Station8, 
                otherOC.StationStatus.Finished);
            oc.StationStatus = status;
            oc.Status = otherOC.Status;
            oc.Week = otherOC.Week;


            return oc;
        }

        private static List<T> convertArrayToList<T>(T[] array)
        {
            List<T> result = new List<T>();
            foreach (T element in array)
                result.Add(element);
            return result;
        }
    }
}
