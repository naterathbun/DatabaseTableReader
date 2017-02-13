using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using DatabaseTableReader.DatabaseClasses;
using System.Data;

namespace DatabaseTableReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            string dataBaseSourceInformation = GetDataBaseSource();

            PopulateLocalDatabase(database, dataBaseSourceInformation);



            Console.ReadLine();
        }


        private static void PopulateLocalDatabase(Database database, string dataBaseSourceInformation)
        {
            using (SqlConnection connection = new SqlConnection(dataBaseSourceInformation))
            {
                connection.Open();
                ReadInformationFromDatabase(database, connection);
            }
        }

        private static void ReadInformationFromDatabase(Database database, SqlConnection connection)
        {
            AddTablesToDatabase(database, connection);
            AddColumnsToTable(database, connection);
            AddRowsToColumns(database, connection);
        }

        private static void AddTablesToDatabase(Database database, SqlConnection connection)
        {
            DataTable schema = connection.GetSchema("Tables");
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                database.AddTable(row[2].ToString());
            }
        }

        private static void AddColumnsToTable(Database database, SqlConnection connection)
        {
            for (int i = 0; i < database.Tables.Count(); i++)
            {
                string[] restrictions = new string[4] { null, null, $"{database.Tables[i].TableName}", null };
                var columnList = connection.GetSchema("Columns", restrictions).AsEnumerable().Select(s => s.Field<String>("Column_Name")).ToList();

                for (int j = 0; j < columnList.Count(); j++)
                {
                    database.Tables[i].AddColumn(columnList[j].ToString());
                }
            }

        }

        private static void AddRowsToColumns(Database database, SqlConnection connection)
        {
            for (int i = 0; i < database.Tables.Count(); i++)
            {
                for (int j = 0; j < database.Tables[i].Columns.Count(); j++)
                {
                    string columnName = database.Tables[i].Columns[j].ColumnHeading;
                    string query = $"SELECT {database.Tables[i].Columns[j].ColumnHeading} FROM {database.Tables[i].TableName}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                database.Tables[i].Columns[j].AddRow(reader[$"{columnName}"].ToString());
                            }
                        }
                    }
                }



            }


        }


        private static void DisplayTablesFromDatabase(Database database)
        {
            for (int i = 0; i < database.Tables.Count(); i++)
            {
                Console.WriteLine(database.Tables[i].TableName);
            }
        }

        private static void DisplayColumnNamesFromTable(Database database, string tableName)
        {
            int tableId = GetTableId(database, tableName);

            for (int i = 0; i < database.Tables[tableId].Columns.Count(); i++)
            {
                Console.WriteLine(database.Tables[tableId].Columns[i].ColumnHeading);
            }


        }

        private static void DisplayRowsFromColumn(Database database, string tableName, string columnName)
        {
            int tableId = GetTableId(database, tableName);
            int columnId = GetColumnId(database, tableId, columnName);

            for (int i = 0; i < database.Tables[tableId].Columns[columnId].Rows.Count(); i++)
            {
                Console.WriteLine(database.Tables[tableId].Columns[columnId].Rows[i]);
            }


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

        private static string GetDataBaseSource()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=world;User ID=te_student;Password=sqlserver1";
        }
    }
}
