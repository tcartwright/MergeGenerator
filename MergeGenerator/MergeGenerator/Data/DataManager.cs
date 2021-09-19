using Dapper;
using MergeGenerator.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeGenerator.Data
{
    static class DataManager
    {
        public static List<SQLDb> GetDbObjects(string serverName)
        {
            string sql = Resources.GetObjects;
            var dict = new Dictionary<string, SQLDb>(StringComparer.InvariantCultureIgnoreCase);

            using (var connection = new SqlConnection($"Integrated Security=SSPI;Initial Catalog=master;Data Source={serverName};"))
            {
                connection.Open();
                var dbs = connection.Query<SQLDb, SQLTable, SQLDb>(sql, (sqldb, sqltable) =>
                        {
                            if (!dict.TryGetValue(sqldb.database_name, out SQLDb db))
                            {
                                db = sqldb;
                                dict.Add(sqldb.database_name, db);
                            }
                            db.Tables.Add(sqltable);
                            return db;
                        },
                    splitOn: "schema_name")
                    .Distinct()
                    .ToList();
                return dbs;
            }
        }
    }
}
