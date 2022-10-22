using ConsoleTableExt;
using System;
using System.Collections.Generic;

namespace Coding_Tracker
{
    internal class TableVisualization
    {
        internal static void ShowTable<T>(List<T> tableData) where T : class
        {
            Console.WriteLine("\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Coding Tracker")
                .ExportAndWriteLine();

            Console.WriteLine("\n");
        }
    }
}