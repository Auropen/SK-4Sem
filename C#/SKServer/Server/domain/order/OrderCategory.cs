using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
{
    public class OrderCategory
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public List<OrderElement> Elements { get; private set; }

        public OrderCategory(string name, int id)
        {
            Elements = new List<OrderElement>();
            Name = name;
            ID = id;
        }
    }
}
