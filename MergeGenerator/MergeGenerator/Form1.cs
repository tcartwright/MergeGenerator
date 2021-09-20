using MergeGenerator.Data;
using MergeGenerator.Properties;
using Microsoft.PowerShell;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MergeGenerator
{
    public partial class Form1 : Form
    {
        private List<SQLDb> _dbs = null;
        private SQLDb _currentDb = null;
        private MergeOptions _mergeOptions;
        private readonly StringComparer _stringComparer = StringComparer.InvariantCultureIgnoreCase;
        private string _module = string.Empty;
        private readonly string _defaultFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{typeof(Form1).Assembly.GetName().Name}\\Output";
        private readonly string _PSEditor = ConfigurationManager.AppSettings["DEFAULT_PS_EDITOR"];
        private readonly string _SqlEditor = ConfigurationManager.AppSettings["DEFAULT_SQL_EDITOR"];
        private readonly string _PSEditorFileName = Path.GetFileName(ConfigurationManager.AppSettings["DEFAULT_PS_EDITOR"]);
        private readonly string _SqlEditorFileName = Path.GetFileName(ConfigurationManager.AppSettings["DEFAULT_SQL_EDITOR"]);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtServer.Text = Settings.Default.ServerName;
            txtOutPutFolder.Text = string.IsNullOrWhiteSpace(Settings.Default.OutputPath) ? _defaultFolder : Settings.Default.OutputPath;

            SetEnabled(false);

            ResizeDescriptionArea(ref pgOptions, 6);
            _module = Regex.Replace(Resources.MergeGeneratorModule, "Export-ModuleMember.*", "");

            this.Text = $"{this.Text} v{typeof(Form1).Assembly.GetName().Version}";
        }

        private bool ResizeDescriptionArea(ref PropertyGrid grid, int lines)
        {
            // TIM C: got the idea from here, but had to re-write to make work https://www.codeproject.com/Articles/28193/Change-the-height-of-a-PropertyGrid-s-description
            try
            {
                Control docComment = grid.Controls.Cast<Control>().First(c => _stringComparer.Equals(c.GetType().FullName, "System.Windows.Forms.PropertyGridInternal.DocComment"));
                Type docCommentType = docComment.GetType();
                var pi = docCommentType.GetProperty("Lines");
                pi.SetValue(docComment, lines, null);

                FieldInfo fi = docCommentType.BaseType.GetField("userSized", BindingFlags.Instance | BindingFlags.NonPublic);
                fi.SetValue(docComment, true);
                return true;
            }
            catch (Exception error)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBoxEx.Show(this, error.Message, "ResizeDescriptionArea()");
                }
                return false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtServer.Text))
                {
                    errorProvider1.SetError(txtServer, "Please enter a server name to connect to.");
                    return;
                }
                errorProvider1.Clear();

                if (txtQuery.Text.Length > 0 && MessageBoxEx.Show(this, "You already have a query loaded, do you still wish to re-load the database objects?", "Re-Load",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                txtQuery.Text = string.Empty;
                _dbs = DataManager.GetDbObjects(txtServer.Text);
                BindMergeOptions();
                cboDatabase.DataSource = _dbs;
                cboDatabase.ValueMember = cboDatabase.DisplayMember = "database_name";

                SetEnabled(true);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBoxEx.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetEnabled(false);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BindMergeOptions()
        {
            pgOptions.SelectedObject = _mergeOptions = new MergeOptions(this);
            var table = cboTables.SelectedItem as SQLTable;

            if (table != null)
            {
                _mergeOptions.IdentityField = table.identity_column;
                _mergeOptions.IdentityInsert = !string.IsNullOrEmpty(table.identity_column);
                pgOptions.Refresh();
            }
        }
        private bool IsValid()
        {
            errorProvider1.Clear();
            bool valid = true;
            if (cboDatabase.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboDatabase, "Please select a database from the list.");
                valid = false;
            }
            if (cboTables.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboTables, "Please select a table from the list.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtQuery.Text))
            {
                errorProvider1.SetError(txtQuery, "Please enter a valid sql query.");
                valid = false;
            }
            if (!Path.IsPathRooted(txtOutPutFolder.Text))
            {
                errorProvider1.SetError(txtOutPutFolder, @"Please enter a valid folder path that is fully rooted using either {drive}:\path or \\servername\share.");
                valid = false;
            }
            else
            {
                try
                {
                    var di = Directory.CreateDirectory(txtOutPutFolder.Text);
                    txtOutPutFolder.Text = di.FullName;
                }
                catch
                {
                    errorProvider1.SetError(txtOutPutFolder, "Please enter a valid folder path.");
                    valid = false;
                }
            }

            return valid;
        }

        private string GetScriptPath(string extension)
        {
            var fileName = GetValidFileName($"{cboDatabase.Text}_{cboTables.Text}_{DateTime.Now:yyyyMMdd}.{extension}");
            return Path.Combine(txtOutPutFolder.Text, fileName);
        }

        private void btnGenerateScript_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid()) { return; }
                Cursor.Current = Cursors.WaitCursor;
                var path = GetScriptPath("ps1");
                var tbl = cboTables.SelectedItem as SQLTable;

                File.WriteAllText(path, _mergeOptions.GenerateScript(txtServer.Text, cboDatabase.Text, tbl.schema_name, tbl.table_name, txtQuery.Text));
                File.WriteAllText(Path.Combine(txtOutPutFolder.Text, "MergeGenerator.psm1"), Resources.MergeGeneratorModule);

                if (MessageBoxEx.Show(this, $"The script was saved to:\r\n\r\n'{path}'\r\n\r\nDo you wish to open this file using {_PSEditorFileName}?", "Saved Script", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var pi = new ProcessStartInfo(_PSEditor, $"{path}")
                    {
                        WindowStyle = ProcessWindowStyle.Maximized
                    };
                    var process = Process.Start(pi);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBoxEx.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private string GetValidFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));

        }

        private void btnMergeGenerator_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid()) { return; }
                Cursor.Current = Cursors.WaitCursor;

                var tbl = cboTables.SelectedItem as SQLTable;
                var path = GetScriptPath("sql");
                var iss = InitialSessionState.CreateDefault();
                iss.ExecutionPolicy = ExecutionPolicy.Bypass;

                using (PowerShell ps = PowerShell.Create(iss))
                {
                    //add the module that we extracted from the resources
                    ps.AddScript(_module);
                    ps.Invoke();
                    //add the script with the function call and invoke it
                    ps.AddScript(Resources.MergeGenerator);
                    ps.Invoke();
                    //clear out the existing commands
                    ps.Commands.Clear();
                    //add the call to the function with all the parameters and invoke it
                    ps.AddCommand("GenMerge")
                        .AddParameter("Server", txtServer.Text)
                        .AddParameter("dbName", cboDatabase.Text)
                        .AddParameter("user", null)
                        .AddParameter("pwd", null)
                        .AddParameter("TargetSchema", tbl.schema_name)
                        .AddParameter("TargetTable", tbl.table_name)
                        .AddParameter("Joins", _mergeOptions.SourceJoins)
                        .AddParameter("On", _mergeOptions.OnClause)
                        .AddParameter("Query", txtQuery.Text)
                        .AddParameter("GenDeleteClause", _mergeOptions.GenerateDeleteClause)
                        .AddParameter("IdentInsert", _mergeOptions.IdentityInsert)
                        .AddParameter("OutputIdent", _mergeOptions.OutputIdentity)
                        .AddParameter("IdentField", _mergeOptions.IdentityField)
                        .AddParameter("FieldReps", _mergeOptions.FieldReplacements)
                        .AddParameter("TurnOffOnTriggers", _mergeOptions.DisableEnableTriggers)
                        .AddParameter("WrapGos", _mergeOptions.WrapGoStatements)
                        .AddParameter("Indent", _mergeOptions.Indentation ?? "")
                        .AddParameter("AllColumns", _mergeOptions.AllColumns)
                        .AddParameter("ExcludeColumns", _mergeOptions.ExcludeColumns)
                        .AddParameter("OutFile", path);

                    var psOut = ps.Invoke();

                    if (ps.HadErrors)
                    {
                        var errors = ps.Streams.Error.ReadAll();
                        foreach (var error in errors)
                        {
                            throw error.Exception;
                        }
                    }

                    if (MessageBoxEx.Show(this, $"The merge script was saved to:\r\n\r\n'{path}'\r\n\r\nDo you wish to open this file using {_SqlEditorFileName}?", "Saved Merge", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Process.Start(_SqlEditor, path);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBoxEx.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            cboDatabase.DataSource = cboTables.DataSource = null;
            cboDatabase.Enabled = cboTables.Enabled = false;

            SetEnabled(false);

            txtQuery.Text = "";
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentDb = cboDatabase.SelectedItem as SQLDb;

            if (_currentDb != null)
            {
                cboTables.DataSource = _currentDb.Tables;
                cboTables.ValueMember = cboDatabase.DisplayMember = "full_name";
                cboTables.Enabled = true;

                errorProvider1.SetError(cboDatabase, null);
            }

        }

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentDb == null) { return; }
            var table = cboTables.SelectedItem as SQLTable;

            if (table != null)
            {
                pgOptions.Enabled = true;
                _mergeOptions.IdentityField = table.identity_column;
                _mergeOptions.IdentityInsert = !string.IsNullOrEmpty(table.identity_column);
                pgOptions.Refresh();
                txtQuery.Text = $"SELECT TOP (100) * FROM {cboTables.Text}";
                errorProvider1.SetError(cboTables, null);
            }
        }
        private void SetEnabled(bool enabled)
        {
            cboDatabase.Enabled =
                cboTables.Enabled =
                txtQuery.Enabled =
                btnMergeGenerator.Enabled =
                btnGenerateScript.Enabled =
                pgOptions.Enabled = 
                txtOutPutFolder.Enabled = enabled;
        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            if (txtQuery.Text.Length > 0)
            {
                errorProvider1.SetError(txtQuery, null);
            }
        }

        private void btnResetOptions_Click(object sender, EventArgs e)
        {
            BindMergeOptions();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ServerName = txtServer.Text;
            Settings.Default.OutputPath = txtOutPutFolder.Text;
            Settings.Default.Save();
        }

        private void changeFolderPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolder(txtOutPutFolder.Text);

            if (CommonFileDialog.IsPlatformSupported)
            {
                using (var dialog = new CommonOpenFileDialog())
                {
                    dialog.IsFolderPicker = true;
                    dialog.Multiselect = false;
                    dialog.DefaultDirectory = txtOutPutFolder.Text;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        txtOutPutFolder.Text = dialog.FileName;
                    }
                }
            }
            else
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.SelectedPath = txtOutPutFolder.Text;
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtOutPutFolder.Text = dialog.SelectedPath;
                    }
                }
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateFolder(txtOutPutFolder.Text);
                Process.Start("explorer.exe", txtOutPutFolder.Text);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, $"Exception opening folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutPutFolder.Text = _defaultFolder;
        }

        private void CreateFolder(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, $"Exception creating folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
