using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Coding_Tracker
{
    internal class CodingController
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        internal void Get()
        {
            List<Coding> tabledata = new List<Coding>();

            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM coding_tracker";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tabledata.Add(new Coding
                                {
                                    ID = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Duration = reader.GetString(2),
                                });

                            }
                        } else
                        {
                            Console.WriteLine("No rows found");
                        }
                    }
                }
                Console.WriteLine("\n\n");
            }
            TableVisualization.ShowTable(tabledata);
        }

        internal Coding GetById(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM coding_tracker WHERE ID = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        Coding coding = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            coding.ID = reader.GetInt32(0);
                            coding.Date = reader.GetString(1);
                            coding.Duration = reader.GetString(2);
                        }
                        Console.WriteLine("\n");
                        return coding;
                    }
                }
            }
        }

        internal void Post(Coding coding)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO coding_tracker (date, duration) VALUES ('{coding.Date}', '{coding.Duration}')";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"DELETE from coding_tracker WHERE ID '{id}'";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }
        internal void Update(Coding coding)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"UPDATE coding_tracker SET Date = '{coding.Date}', Duration = '{coding.Duration}' WHERE ID = {coding.ID}";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }

    }
}