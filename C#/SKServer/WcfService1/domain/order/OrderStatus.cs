using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.domain.order
{
    [DataContract]
    public class OrderStatus
    {
        [DataMember]
        public string Station4 { get; set; }
        [DataMember]
        public string Station5 { get; set; }
        [DataMember]
        public string Station6 { get; set; }
        [DataMember]
        public string Station7 { get; set; }
        [DataMember]
        public string Station8 { get; set; }
        [DataMember]
        public bool Finished { get; set; }
    }
}