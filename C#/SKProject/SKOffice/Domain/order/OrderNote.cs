using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.domain.order
{
    [DataContract]
    public class OrderNote
    {
        [DataMember]
        public string Text { get; set; }

        public OrderNote(string text)
        {
            Text = text;
        }
    }
}