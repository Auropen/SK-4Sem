using SKOffice.domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
{
    public class OrderElement
    {
        public string Position { get; set; }
        public string Text { get; set; }
        public string Hinge { get; set; }
        public string Finish { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public OrderType Type { get; set; }

        public OrderElement(string position, string text, string hinge, string finish, string amount, string unit, OrderType type)
        {
            Position = position;
            Text = text;
            Hinge = hinge;
            Finish = finish;
            Amount = amount;
            Unit = unit;
            Type = type;
        }
    }
}
