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
        private string columnHeading;
        private List<string> rows = new List<string>();

        public Column(string columnHeading)
        {
            this.columnHeading = columnHeading;
        }

    }
}
