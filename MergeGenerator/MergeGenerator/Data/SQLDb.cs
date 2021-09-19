using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeGenerator.Data
{
    class SQLDb
    {
        public string database_name { get; set; }
        public List<SQLTable> Tables { get; set; } = new List<SQLTable>();

        public override string ToString()
        {
            return this.database_name ?? base.ToString();
        }
    }
}
