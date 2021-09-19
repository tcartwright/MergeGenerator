using MergeGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergeGenerator.Data
{
    class MergeOptions
    {

        private bool _generateDeleteClause;
        private readonly IWin32Window _owner;

        public MergeOptions(IWin32Window owner)
        {
            this._owner = owner;
        }

        [Browsable(true)]
        [Description("The ON clause used by the MERGE statement to determine INSERT/UPDATE/DELETE. Can be generated using the function Get-TableOnClause. The two tables will be aliased as [source] and [target]. If not passed in, then the the default ON clause will be generated which uses the primary key.")]
        [Category("OnClause")]
        [DefaultValue(null)]
        public string OnClause { get; set; }

        [Browsable(true)]
        [Description("If AllColumns is true, then all of the columns in the table will be used for the on clause, else just the primary key columns will be used. Only takes effect if the OnClause is empty.")]
        [Category("OnClause")]
        [DefaultValue(false)]
        public bool AllColumns { get; set; }

        [Browsable(true)]
        [Description("If AllColumns is true then this list can be used to exclude columns. Only takes effect if the OnClause is empty.")]
        [Category("OnClause")]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> ExcludeColumns { get; set; } = new List<string>();

        [Browsable(true)]
        [Description("If true the delete clause will be added for the MERGE.")]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool GenerateDeleteClause
        {
            get => _generateDeleteClause;
            set
            {
                //if switching from false to true, then warn the user of possible data loss
                if (!_generateDeleteClause && value)
                {
                    if (MessageBoxEx.Show(_owner, "WARNING: By enabling the delete clause you can possibly introduce possible data loss against the target table.", "WARNING: Possible Data Loss", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                _generateDeleteClause = value;
            }
        }

        [Browsable(true)]
        [Description("If true, then IDENTITY INSERT will be enabled for the duration of the MERGE.")]
        [Category("Identity")]
        [DefaultValue(false)]
        public bool IdentityInsert { get; set; }

        [Browsable(true)]
        [Description("If true, the old and possibly new identity will be output to a table variable. To use this functionality, the old identity value must be aliased as original_identity. Then the table variable can be used in subsequent queries to join with. You would use the SourceJoins clause to join to the original_identity and then use the new identity in the merge.")]
        [Category("Identity")]
        [DefaultValue(false)]
        public bool OutputIdentity { get; set; }

        [Browsable(true)]
        [Description("The name of the identity field on the table.")]
        [Category("Identity")]
        [DefaultValue(null)]
        public string IdentityField { get; set; }

        [Browsable(true)]
        [Description("These field replacements are used in combination with the SourceJoins and OutputIdentity properties. Allows the replacement of fields to be remapped from the source table to the join table in the SourceJoins clause.")]
        [Category("Source Data")]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> FieldReplacements { get; set; } = new List<string>();

        [Browsable(true)]
        [Description("Additional joins that can be added to the source table to pull in extra data for the source data.")]
        [Category("Source Data")]
        [DefaultValue(null)]
        public string SourceJoins { get; set; }

        [Browsable(true)]
        [Description("If true, then all of the triggers on the table will be disabled for the duration of the MERGE.")]
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool DisableEnableTriggers { get; set; } = true;

        [Browsable(true)]
        [Description("If true, then GO statements will be wrapped around the entire MERGE.")]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool WrapGoStatements { get; set; }

        [Browsable(true)]
        [Description("Allows for extra indentation to be added.")]
        [Category("Behavior")]
        [DefaultValue(null)]
        public string Indentation { get; set; }

        public string GenerateScript(string server, string database, string tableSchema, string tableName, string query)
        {
            var props = this.GetType().GetProperties();

            var ret = Resources.ScriptTemplate
                .Replace("#ServerInstance#", server)
                .Replace("#Database#", database)
                .Replace("#DestinationSchemaName#", tableSchema)
                .Replace("#DestinationTableName#", tableName)
                .Replace("#SourceQuery#", query);

            foreach (var prop in props)
            {
                var name = prop.Name;
                if (prop.PropertyType == typeof(string))
                {
                    var val = prop.GetValue(this) as string;
                    ret = ret.Replace($"\"#{name}#\"", string.IsNullOrWhiteSpace(val) ? "$null" : $"\"{val}\"");
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    var val = Convert.ToBoolean(prop.GetValue(this));
                    ret = ret.Replace($"#{name}#", $"${val}".ToLower());
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    var val = prop.GetValue(this) as List<string>;
                    ret = ret.Replace($"#{name}#", val.Any() ? $"@(\"{string.Join("\", \"", val)}\")" : "$null");
                }
            }

            return ret;
        }
    }
}
