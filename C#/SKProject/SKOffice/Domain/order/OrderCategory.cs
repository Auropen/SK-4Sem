﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
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
        
        public OrderCategory(string name, int id)
        {
            Elements = new List<OrderElement>();
            Name = name;
            ID = id;
        }
    }
}
