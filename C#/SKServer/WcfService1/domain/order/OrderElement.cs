using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.domain.order
{
    [DataContract]
    public class OrderElement
    {
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public List<string> ElementInfo { get; private set; }
        [DataMember]
        public string Hinge { get; set; }
        [DataMember]
        public string Finish { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public bool[] StationStatus { get; set; }

        public OrderElement(string position, string hinge, string finish, string amount, string unit)
        {
            Position = position;
            ElementInfo = new List<string>();
            Hinge = hinge;
            Finish = finish;
            Amount = amount;
            Unit = unit;
            StationStatus = new bool[5];
        }

        public void updateStatus(int stationNumber, bool status)
        {
            StationStatus[stationNumber - 4] = status;
        }

        public bool allDone()
        {
            return StationStatus[0] && StationStatus[1] && StationStatus[2] && StationStatus[3] && StationStatus[4];
        }
    }
}
