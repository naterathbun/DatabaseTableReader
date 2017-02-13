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
    public class Column
    {
        public string ColumnHeading { get; set; }
        public List<string> Rows { get; set; } = new List<string>();

        public Column(string columnHeading)
        {
            this.ColumnHeading = columnHeading;
        }

        public void AddRow(string newRow)
        {
            this.Rows.Add(newRow);
        }

    }
}
