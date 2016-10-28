using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGRoom
    {
        /// System
        public HGSystem System;
        
        /// Features
        public List<HGFeature> Features = new List<HGFeature>();

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
        public HGRoom(HGSystem system, string name, string comments="")
        {
            System = system;
            Name = name;
            Comments = comments;
        }


        /**
         * Add feature.
         */
        public void addFeature(string name, string comments="")
        {
            Features.Add(new HGFeature(System, name, this, comments));
        }


        /**
         * Feature names.
         */
        public List<string> FeatureNames()
        {
            List<string> names = new List<string>();
            foreach (HGFeature f in Features)
            {
                names.Add(f.Name.ToLower());
            }
            return names;
        }
    }
}
