using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace HigherGroundsRouteManagement
{
    class HGDatabase
    {
        #region construct
        /// Higher Ground System
        private HGSystem mSystem;

        /// SQLite database file
        private string mSqliteHGFileName = "higherGroundsSystemDatabase.sqlite3";
        private string mDatabaseVersion = "3";
        public string DatabaseName
        {
            get { return this.mSqliteHGFileName; }
        }
        public string ConnectionString
        {
            get { return "Data Source=" + DatabaseName + ";Version=" + this.mDatabaseVersion + ";"; }
        }

        /// Database connection object
        private SQLiteConnection mConnection;

        /**
         * Constructor.
         */
        public HGDatabase(HGSystem s)
        {
            this.mSystem = s;
            this.createFileIfNeeded();
            this.initializeQuery();
        }

        #endregion

        #region create

        /**
         * Create the database file if needed.
         */
        public void createFileIfNeeded()
        {
            if (!File.Exists(DatabaseName))
            {
                SQLiteConnection.CreateFile(DatabaseName);
                this.createTables();
            }
        }


        /**
         * Create the initial tables in the database.
         */
        public void createTables()
        {
            string sql = "create table rooms (name varchar(100), comments varchar(200))";
            if (!this.executeNonQueryCommand(sql)) { Environment.Exit(2); }

            sql = "create table setters (name varchar(100))";
            if (!this.executeNonQueryCommand(sql)) { Environment.Exit(2); }

            sql = "create table grades (name varchar(100), type varchar(100), comments varcar(200))";
            if (!this.executeNonQueryCommand(sql)) { Environment.Exit(2); }

            sql = "create table features (name varchar(100), room varchar(100), comments varchar(200))";
            if (!this.executeNonQueryCommand(sql)) { Environment.Exit(2); }

            sql = "create table routes (name varchar(100), grade varchar(100), feature varchar(100), setter varchar(100), date varchar(100), comments varchar(200))";
            if (!this.executeNonQueryCommand(sql)) { Environment.Exit(2); }

            Console.WriteLine("Created database tables: Rooms, Setters, Grades, Features, and Routes. Starting with NO DATA!");
        }

        #endregion

        #region insert

        /**
         * Insert a room into the table.
         */
        public bool insertRoom(string name, string comments="")
        {
            string sql = "insert into rooms (name, comments) values ('" + name + "', '" + comments + "')";
            return executeNonQueryCommand(sql);
        }


        /**
         * Insert a setter into the table.
         */
        public bool insertSetter(string name)
        {
            string sql = "insert into setters (name) values ('" + name + "')";
            return executeNonQueryCommand(sql);
        }


        /**
         * Insert a grade into the table.
         */
        public bool insertGrade(string name, string type, string comments = "")
        {
            string sql = "insert into grades (name, type, comments) values ('" + name + "', '" + type + "', '" + comments + "')";
            return executeNonQueryCommand(sql);
        }


        /**
         * Insert a feature into the table.
         */
        public bool insertFeature(string name, string room, string comments = "")
        {
            string sql = "insert into features (name, room, comments) values ('" + name + "', '" + room + "', '" + comments + "')";
            return executeNonQueryCommand(sql);
        }


        /**
         * Insert a route into the table.
         */
        public bool insertRoute(string name, string grade, string feature, string setter, string date, string comments = "")
        {
            string sql = "insert into routes (name, grade, feature, setter, date, comments) values ('" + name + "', '" + grade + "', '" + feature + "', '" + setter + "', '" + date + "', '" + comments + "')";
            return executeNonQueryCommand(sql);
        }

        #endregion

        #region delete

        /**
         * Delete room from rooms.
         */
        public bool deleteRoom(string name)
        {
            return this.delete("rooms", "name = " + name);
        }


        /**
         * Delete setter from setters.
         */
        public bool deleteSetter(string name)
        {
            return this.delete("setters", "name = " + name);
        }


        /**
         * Delete grade from grades.
         */
        public bool deleteGrade(string name)
        {
            return this.delete("grades", "name = " + name);
        }


        /**
         * Delete feature from features.
         */
        public bool deleteFeature(string name)
        {
            return this.delete("features", "name = " + name);
        }


        /**
         * Delete route from routes.
         */
        public bool deleteRoute(string name)
        {
            return this.delete("routes", "name = " + name);
        }


        /**
         * Delete room from tables.
         */
        public bool delete(string table, string where)
        {
            string sql = "delete from " + table + " where " + where;
            return executeNonQueryCommand(sql);
        }

        #endregion

        #region query

        /**
         * Query init.
         */
        public void initializeQuery()
        {
            this.getAllRooms();
            this.getAllSetters();
            this.getAllGrades();
            this.getAllFeatures();
            this.getAllRoutes();
        }


        /**
         * Query get all rooms.
         */
        public void getAllRooms()
        {
            string sql = "select * from rooms";
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    mSystem.addRoom(reader["name"].ToString(), reader["comments"].ToString());
                mConnection.Close();
            } catch (Exception e)
            {
                Console.WriteLine("Error: could not get all rooms -- " + e.ToString());
            }
        }


        /**
         * Query get all setters.
         */
        public void getAllSetters()
        {
            string sql = "select * from setters";
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    mSystem.addSetter(reader["name"].ToString());
                mConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: could not get all setters -- " + e.ToString());
            }
        }


        /**
         * Query get all grades. TODO: ADD FUNCTIONALITY FOR GRADES
         */
        public void getAllGrades()
        {
            string sql = "select * from grades";
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    mSystem.addGrade(reader["name"].ToString(), reader["type"].ToString(), reader["comments"].ToString());
                mConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: could not get all grades -- " + e.ToString());
            }
        }


        /**
         * Query get all features.
         */
        public void getAllFeatures()
        {
            string sql = "select * from features";
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    mSystem.addFeature(reader["room"].ToString(), reader["name"].ToString(), reader["comments"].ToString());
                mConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: could not get all features -- " + e.ToString());
            }
        }


        /**
         * Query get all routes.
         */
        public void getAllRoutes()
        {
            string sql = "select * from routes";
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    mSystem.addRoute(reader["feature"].ToString(), reader["name"].ToString(),
                        reader["grade"].ToString(), reader["setter"].ToString(), reader["date"].ToString(),
                        reader["comments"].ToString());
                mConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: could not get all routes -- " + e.ToString());
            }
        }

        #endregion

        #region execute

        /**
         * Execute a non-query command.
         */
        public bool executeNonQueryCommand(string sql)
        {
            try
            {
                mConnection = new SQLiteConnection(ConnectionString);
                mConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, mConnection);
                command.ExecuteNonQuery();
                mConnection.Close();
            } catch (Exception e)
            {
                Console.WriteLine("Error: While executing non-query sql -- " + e.ToString());
                return false;
            }
            return true;
        }

        #endregion
    }
}
