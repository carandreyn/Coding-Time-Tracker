using Microsoft.Data.Sqlite;
using System;

namespace Coding_Tracker
{
    internal class DatabaseManager
    {
        internal static void createTable(string connectionString)
        {
            using(var connection = new SqliteConnection(connectionString))
            {                
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding_tracker (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Duration TEXT)";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }
    }
}