using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKOffice
{
    public class OrderHandler
    {
        private OrderHandler instance;

        public OrderHandler Instance
        {
            get
            {
                if (instance == null) Instance = new OrderHandler();
                return Instance;
            }
            private set
            {
                instance = value;
            }
        }
        
        /// <summary>
        /// Singleton constructor, can only be called from within.
        /// </summary>
        private OrderHandler()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void readRawFile(string url)
        {

        }
    }
}
