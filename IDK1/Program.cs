using System.Data.SQLite;
using System.Diagnostics;

namespace IDK1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            
            if (!File.Exists(SQLMethods.dbpath)) {
                // Creates the database if it doesn't exist
                SQLiteConnection sqlite_conn;
                sqlite_conn = SQLMethods.CreateConnection();
                CreateTable(sqlite_conn);
                InsertData(sqlite_conn);
            }
        }

        

        static void CreateTable(SQLiteConnection conn) {
            SQLiteCommand sqlite_cmd;
            string Sql = "CREATE TABLE IF NOT EXISTS Color(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Color VARCHAR(24) NOT NULL)";
            string Sql1 = "CREATE TABLE IF NOT EXISTS Sex(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Sex VARCHAR(10) NOT NULL)";
            string Sql2 = "CREATE TABLE IF NOT EXISTS Entity(ID VARCHAR(10) NOT NULL, Birth DATE NOT NULL, Sex INTEGER NOT NULL, Death DATE, Color INTEGER NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (Color) REFERENCES Color(ID), FOREIGN KEY (Sex) REFERENCES Sex(ID))";
            string Sql3 = "CREATE TABLE IF NOT EXISTS Parent(ChildID VARCHAR(10) NOT NULL, ParentID VARCHAR(10), Known INTEGER NOT NULL, PRIMARY KEY(ChildID, ParentID), FOREIGN KEY (ChildID) REFERENCES Entity(ID), FOREIGN KEY (ParentID) REFERENCES Entity(ID))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Sql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql1;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql2;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql3;
            sqlite_cmd.ExecuteNonQuery();
        }

        static void InsertData(SQLiteConnection conn) {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Sex (Sex) VALUES ('Unknown'),('Male'),('Female');";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Color (Color) VALUES ('Light Blue'),('Light Yellow'),('Light Green'),('Light Grey'),('Light Orange');";
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}