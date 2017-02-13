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
        public string TableName { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();

        public Table(string tableName)
        {
            this.TableName = tableName;
        }

        public void AddColumn(string columnName)
        {
            this.Columns.Add(new Column(columnName));
        }

    }
}
