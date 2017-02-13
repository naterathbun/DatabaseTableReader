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
    public class Table
    {
        private string tableName;
        private List<Column> columns = new List<Column>();

        public Table(string tableName)
        {
            this.tableName = tableName;
        }

        public string GetTableName()
        {
            return this.tableName;
        }

    }
}
