﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice.domain.order
{
    public class OrderElement
    {
        public string Position { get; set; }
        public List<string> ElementInfo { get; private set; }
        public string Hinge { get; set; }
        public string Finish { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }

        public OrderElement(string position, string hinge, string finish, string amount, string unit)
        {
            Position = position;
            ElementInfo = new List<string>();
            Hinge = hinge;
            Finish = finish;
            Amount = amount;
            Unit = unit;
        }
    }
}