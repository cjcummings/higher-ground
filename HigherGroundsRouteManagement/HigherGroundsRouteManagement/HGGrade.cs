using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGGrade
    {
        public enum Types
        {
            boulder,
            rope
        }

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

        /// Type
        public Types Type;

        /**
         * Constructor.
         */
        public HGGrade(string name, string type, string comments="")
        {
            Name = name;
            if (type.ToLower() == "rope")
                Type = Types.rope;
            else Type = Types.boulder;
            Comments = comments;
        }
    }
}
