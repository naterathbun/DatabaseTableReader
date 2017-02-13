using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using DatabaseTableReader.DatabaseClasses;

namespace DatabaseTableReader
{
    public class Database
    {
        private List<Table> tables = new List<Table>();

        public Database()
        {

        }

        public void AddTable(string tableName)
        {
            this.tables.Add(new Table(tableName));
        }

        public List<Table> GetTablesList()
        {
            return this.tables;
        }
    }
}
