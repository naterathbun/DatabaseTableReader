using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace DatabaseTableReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataBaseSourceInformation = GetDataBaseSource();

            using (SqlConnection sqlConnection = new SqlConnection(dataBaseSourceInformation))
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM country;", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(Convert.ToString(reader["name"]));
                }

            }
        }

        private static string GetDataBaseSource()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=world;User ID=te_student;Password=sqlserver1";
        }
    }
}
