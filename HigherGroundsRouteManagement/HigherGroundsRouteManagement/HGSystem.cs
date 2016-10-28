using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherGroundsRouteManagement
{
    class HGSystem
    {
        /// Database
        private HGDatabase mDatabase;

        /// Rooms
        private List<HGRoom> mRooms = new List<HGRoom>();

        /// Grades
        public List<HGGrade> Grades = new List<HGGrade>();

        /// Setters
        public List<HGSetter> Setters = new List<HGSetter>();

        /**
         * Constructor.
         */
        public HGSystem()
        {
            this.mDatabase = new HGDatabase(this);
            //this.testDatabase();
        }


        /**
         * Test database.
         */
        public void testDatabase()
        {
            Console.WriteLine("Before:");
            foreach (HGSetter s in Setters) Console.WriteLine(s.Name.ToString());
            this.AddAndInsertSetter("CJ");
            this.AddAndInsertSetter("Paulie");
            this.AddAndInsertSetter("Carl");
            Console.WriteLine("After:");
            foreach (HGSetter s in Setters) Console.WriteLine(s.Name.ToString());
        }

        #region Add From User

        /**
         * User add setter.
         */
        public void AddAndInsertSetter(string name)
        {
            this.mDatabase.insertSetter(name);
            this.addSetter(name);
        }


        /**
         * User add grade.
         */
        public void AddAndInsertGrade(string name, string type, string comments = "")
        {
            this.mDatabase.insertGrade(name, type, comments);
            this.addGrade(name, type, comments);
        }


        /**
         * User add room.
         */
        public void AddAndInsertRoom(string name, string comments = "")
        {
            this.mDatabase.insertRoom(name, comments);
            this.addRoom(name, comments);
        }


        /**
         * User add feature to a room.
         */
        public void AddAndInsertFeature(string roomName, string featureName, string featureComments)
        {
            this.mDatabase.insertFeature(featureName, roomName, featureComments);
            this.addFeature(roomName, featureName, featureComments);
        }


        /**
         * User add a route to a feature.
         */
        public void AddAndInsertRoute(string featureName, string routeName, string routeGrade, string setter, string date, string comments = "")
        {
            this.mDatabase.insertRoute(routeName, routeGrade, featureName, setter, date, comments);
            this.addRoute(featureName, routeName, routeGrade, setter, date, comments);
        }

        #endregion

        #region Add From Database

        /**
         * Add setter.
         */
        public void addSetter(string name)
        {
            Setters.Add(new HGSetter(this, name));
        }


        /**
         * Add grade.
         */
        public void addGrade(string name, string type, string comments="")
        {
            Grades.Add(new HGGrade(name, type, comments));
        }


        /**
         * Add room.
         */
        public void addRoom(string name, string comments="")
        {
            mRooms.Add(new HGRoom(this, name, comments));
        }


        /**
         * Add feature to a room. MUST HAVE ROOMS FIRST. Cannot have duplicate feature names.
         */
        public void addFeature(string roomName, string featureName, string featureComments)
        {
            foreach (HGRoom room in mRooms)
            {
                if (room.Name.ToLower() == roomName.ToLower() && !room.FeatureNames().Contains(featureName.ToLower()))
                {
                    room.addFeature(featureName, featureComments);
                }
            }
        }


        /**
         * Add a route to a feature. Assuming feature names are never repeated in seperate rooms!!!!!!!
         */
        public void addRoute(string featureName, string routeName, string routeGrade, string setter, string date, string comments="")
        {
            foreach (HGRoom room in mRooms)
            {
                if (room.FeatureNames().Contains(featureName.ToLower()))
                {
                    foreach (HGFeature f in room.Features)
                    {
                        if (f.Name.ToLower() == featureName.ToLower())
                        {
                            f.addRoute(routeName, routeGrade, setter, date, comments);
                            return;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
