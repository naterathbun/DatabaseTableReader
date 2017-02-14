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

            GetInformationFromDatabase(database, dataBaseSourceInformation);

            MenuCLI.OpenMenu(database);
        }


        private static void GetInformationFromDatabase(Database database, string dataBaseSourceInformation)
        {
            using (SqlConnection connection = new SqlConnection(dataBaseSourceInformation))
            {
                connection.Open();
                PopulateDatabaseClasses(database, connection);
            }
        }

        private static void PopulateDatabaseClasses(Database database, SqlConnection connection)
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
        

        private static string GetDataBaseSource()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=world;User ID=te_student;Password=sqlserver1";
        }
    }
}
