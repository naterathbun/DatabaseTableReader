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
            
            using (SqlConnection connection = new SqlConnection(dataBaseSourceInformation))
            {
                connection.Open();
                AddTablesToDatabase(database, connection);

                AddColumnsToTable();
                // Add Column rows
                
                // Display Column Names for a specific table
                // Display rows for a specific column in a specific table

            }


            Console.ReadLine();
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

        private static void AddColumnsToTable()
        {
            // add looped action here to generate new columns list for each table




        }



        private static void DisplayTablesFromDatabase(Database database)
        {
            for (int i = 0; i < database.GetTablesList().Count(); i++)
            {
                Console.WriteLine(database.GetTablesList()[i].GetTableName());
            }
        }



        private static void PrintStuffChangeMeToAGoodName(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM country;", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(Convert.ToString(reader["name"]));
            }
        }

        private static string GetDataBaseSource()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=world;User ID=te_student;Password=sqlserver1";
        }
    }
}
