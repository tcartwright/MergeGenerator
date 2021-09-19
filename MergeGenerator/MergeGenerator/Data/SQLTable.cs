namespace MergeGenerator.Data
{
    public class SQLTable
    {
        public string schema_name { get; set; }
        public string table_name { get; set; }
        public string identity_column { get; set; }

        public string full_name
        {
            get
            {
                return $"{schema_name}.{table_name}";
            }
        }

        public override string ToString()
        {
            return $"{schema_name}.{table_name}";
        }
    }
}