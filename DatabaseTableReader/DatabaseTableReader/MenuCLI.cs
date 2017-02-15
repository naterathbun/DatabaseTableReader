using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTableReader.DatabaseClasses;

namespace DatabaseTableReader
{
    public static class MenuCLI
    {
        public static void OpenMenu(Database database)
        {
            while (true)
            {
                ShowMenuHeader(database);
                string tableName = Console.ReadLine();
                DisplayTableContents(database, tableName);
                Console.ReadLine();
            }

        }

        private static void ShowMenuHeader(Database database)
        {
            Console.Clear();
            Console.WriteLine("SQL Database Information Reader");
            Console.WriteLine("-------------------------------");
            DisplayTablesFromDatabase(database);
            Console.WriteLine("\n-------------------------------");
            Console.Write("Choose a Table from above to Display: ");
        }


        private static void DisplayTablesFromDatabase(Database database)
        {
            for (int i = 0; i < database.Tables.Count(); i++)
            {
                Console.Write(database.Tables[i].TableName + " | ");
            }
        }

        private static void DisplayTableContents(Database database, string tableName)
        {
            int tableId = GetTableId(database, tableName);

            DisplayColumnHeadings(database, tableId);
            DisplayRowContents(database, tableId);
        }

        private static void DisplayRowContents(Database database, int tableId)
        {
            for (int i = 0; i < database.Tables[tableId].Columns[0].Rows.Count(); i++)
            {
                for (int j = 0; j < database.Tables[tableId].Columns.Count(); j++)
                {
                    Console.Write(database.Tables[tableId].Columns[j].Rows[i].ToString());
                    Console.Write(" | ");
                }
                Console.Write("\n");
            }
        }

        private static void DisplayColumnHeadings(Database database, int tableId)
        {
            Console.WriteLine("-----------------------------------------------------");
            for (int j = 0; j < database.Tables[tableId].Columns.Count(); j++)
            {
                Console.Write(database.Tables[tableId].Columns[j].ColumnHeading.ToString());
                Console.Write(" | ");
            }
            Console.Write("\n");
        }


        private static int GetTableId(Database database, string tableName)
        {
            int tableID = 0;

            for (int i = 0; i < database.Tables.Count(); i++)
            {
                if (database.Tables[i].TableName == tableName)
                {
                    tableID = i;
                }
            }

            return tableID;
        }

        private static int GetColumnId(Database database, int tableId, string columnName)
        {
            int columnId = 0;

            for (int i = 0; i < database.Tables[tableId].Columns.Count(); i++)
            {
                if (database.Tables[tableId].Columns[i].ColumnHeading == columnName)
                {
                    columnId = i;
                }
            }

            return columnId;
        }

    }
}
