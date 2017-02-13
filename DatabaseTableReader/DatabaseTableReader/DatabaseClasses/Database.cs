using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using DatabaseTableReader.DatabaseClasses;

namespace DatabaseTableReader.DatabaseClasses
{
    public class Database
    {
        public List<Table> Tables { get; set; } = new List<Table>();

        public void AddTable(string tableName)
        {
            this.Tables.Add(new Table(tableName));
        }

    }
}
