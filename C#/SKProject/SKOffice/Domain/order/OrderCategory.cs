using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfService.domain.order
{
    [DataContract]
    public class OrderCategory
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Parser ID is used to assemble the categories when reading the E02 file.
        /// </summary>
        public int ParserID { get; set; }
        [DataMember]
        public List<OrderElement> Elements { get; private set; }
        
        public OrderCategory(string name)
        {
            Elements = new List<OrderElement>();
            Name = name;
        }
        public OrderCategory(string name, int parserID)
        {
            Elements = new List<OrderElement>();
            Name = name;
            ParserID = parserID;
        }
    }
}
