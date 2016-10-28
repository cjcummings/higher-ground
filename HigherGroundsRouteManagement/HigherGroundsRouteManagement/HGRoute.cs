using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGRoute
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

        /// Comments
        private string mComments;
        public string Comments
        {
            get { return this.mComments; }
            set { this.mComments = value; }
        }

        /// Date
        private DateTime mDate;
        public DateTime Date
        {
            get { return this.mDate; }
            set { this.mDate = value; }
        }

        /// Grade
        private HGGrade mGrade;
        public HGGrade Grade
        {
            get { return this.mGrade; }
            set { this.mGrade = value; }
        }

        /// Setter
        private HGSetter mSetter;
        public HGSetter Setter
        {
            get { return this.mSetter; }
            set { this.mSetter = value; }
        }


        /**
         * Constructor.
         */
        public HGRoute(HGSystem system, string name, string date, string grade, string setter, string comments="")
        {
            System = system;
            Name = name;
            Date = Convert.ToDateTime(date);
            foreach (HGGrade g in System.Grades)
            {
                if (g.Name.ToLower() == grade.ToLower())
                {
                    Grade = g;
                    break;
                }
            }
            foreach (HGSetter s in System.Setters)
            {
                if (s.Name.ToLower() == setter.ToLower())
                {
                    Setter = s;
                    break;
                }
            }
            Comments = comments;
        }
    }
}
