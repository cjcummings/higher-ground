using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGFeature
    {
        /// System
        public HGSystem System;

        /// Routes
        public List<HGRoute> Routes = new List<HGRoute>();

        /// <summary>
        /// Room
        /// </summary>
        private HGRoom mRoom;
        public HGRoom Room
        {
            get { return this.mRoom; }
            set { this.mRoom = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        private string mName;
        public string Name
        {
            get { return this.mName; }
            set { this.mName = value; }
        }

        /// <summary>
        /// Comments
        /// </summary>
        private string mComments;
        public string Comments
        {
            get { return this.mComments; }
            set { this.mComments = value; }
        }


        /**
         * Constructor.
         */
        public HGFeature(HGSystem system, string name, HGRoom room, string comments="")
        {
            System = system;
            Name = name;
            Room = room;
            Comments = comments;
        }


        /**
         * Add route to feature.
         */
        public void addRoute(string routeName, string routeGrade, string setter, string date, string comments="")
        {
            //TODO: ADD ROUTE TO FEATURE
        }
    }
}
