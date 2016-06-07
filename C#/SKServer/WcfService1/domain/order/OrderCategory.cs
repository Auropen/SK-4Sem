using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.domain.order
{
    [DataContract]
    public class OrderCategory
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public List<OrderElement> Elements { get; private set; }
        
        public OrderCategory(int id, string name)
        {
            Elements = new List<OrderElement>();
            Name = name;
            ID = id;
        }
    }
}
