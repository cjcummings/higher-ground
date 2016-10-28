using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGSetter
    {
        /// System
        public HGSystem System;

        /// Name
        private string mName;
        public string Name
        {
            get { return this.mName; }
            set { this.mName = value; }
        }

        /**
         * Constructor.
         */
        public HGSetter(HGSystem sys, string name)
        {
            System = sys;
            Name = name;
        }
    }
}
