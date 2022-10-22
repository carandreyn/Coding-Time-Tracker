using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Coding_Tracker
{
    internal class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            GetUserInput getUserInput = new();

            DatabaseManager.createTable(connectionString);
            getUserInput.MainMenu();

        }
    }
}
