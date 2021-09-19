namespace MergeGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pgOptions = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnMergeGenerator = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnGenerateScript = new System.Windows.Forms.Button();
            this.btnResetOptions = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOutPutFolder = new System.Windows.Forms.TextBox();
            this.cmsFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeFolderPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.cmsFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.Location = new System.Drawing.Point(147, 17);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(400, 22);
            this.txtServer.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtServer, "The SQL Server to connect to");
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(557, 15);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(121, 28);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database:";
            // 
            // cboDatabase
            // 
            this.cboDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(148, 48);
            this.cboDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(399, 24);
            this.cboDatabase.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cboDatabase, "The database");
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // cboTables
            // 
            this.cboTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTables.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTables.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(147, 81);
            this.cboTables.Margin = new System.Windows.Forms.Padding(4);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(399, 24);
            this.cboTables.TabIndex = 6;
            this.toolTip1.SetToolTip(this.cboTables, "The table that the merge will be generated for. It can also be used as the source" +
        " query, or the VALUES clause could be used instead.");
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Destination Table:";
            // 
            // pgOptions
            // 
            this.pgOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgOptions.Location = new System.Drawing.Point(708, 35);
            this.pgOptions.Margin = new System.Windows.Forms.Padding(4);
            this.pgOptions.Name = "pgOptions";
            this.pgOptions.Size = new System.Drawing.Size(395, 464);
            this.pgOptions.TabIndex = 7;
            this.pgOptions.ToolbarVisible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Source Query:";
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(9, 162);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(668, 337);
            this.txtQuery.TabIndex = 10;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // btnMergeGenerator
            // 
            this.btnMergeGenerator.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMergeGenerator.Location = new System.Drawing.Point(195, 507);
            this.btnMergeGenerator.Margin = new System.Windows.Forms.Padding(4);
            this.btnMergeGenerator.Name = "btnMergeGenerator";
            this.btnMergeGenerator.Size = new System.Drawing.Size(140, 28);
            this.btnMergeGenerator.TabIndex = 11;
            this.btnMergeGenerator.Text = "Generate Merge";
            this.btnMergeGenerator.UseVisualStyleBackColor = true;
            this.btnMergeGenerator.Click += new System.EventHandler(this.btnMergeGenerator_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(877, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Options:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnGenerateScript
            // 
            this.btnGenerateScript.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGenerateScript.Location = new System.Drawing.Point(343, 507);
            this.btnGenerateScript.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateScript.Name = "btnGenerateScript";
            this.btnGenerateScript.Size = new System.Drawing.Size(140, 28);
            this.btnGenerateScript.TabIndex = 13;
            this.btnGenerateScript.Text = "Generate PS Script";
            this.btnGenerateScript.UseVisualStyleBackColor = true;
            this.btnGenerateScript.Click += new System.EventHandler(this.btnGenerateScript_Click);
            // 
            // btnResetOptions
            // 
            this.btnResetOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetOptions.Location = new System.Drawing.Point(835, 507);
            this.btnResetOptions.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetOptions.Name = "btnResetOptions";
            this.btnResetOptions.Size = new System.Drawing.Size(140, 28);
            this.btnResetOptions.TabIndex = 14;
            this.btnResetOptions.Text = "Reset Options";
            this.btnResetOptions.UseVisualStyleBackColor = true;
            this.btnResetOptions.Click += new System.EventHandler(this.btnResetOptions_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Output Folder:";
            // 
            // txtOutPutFolder
            // 
            this.txtOutPutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutPutFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtOutPutFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtOutPutFolder.ContextMenuStrip = this.cmsFolder;
            this.txtOutPutFolder.Location = new System.Drawing.Point(148, 113);
            this.txtOutPutFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutPutFolder.Name = "txtOutPutFolder";
            this.txtOutPutFolder.ReadOnly = true;
            this.txtOutPutFolder.Size = new System.Drawing.Size(400, 22);
            this.txtOutPutFolder.TabIndex = 15;
            this.toolTip1.SetToolTip(this.txtOutPutFolder, "The folder to save all generated scripts to. Right click for more options.");
            // 
            // cmsFolder
            // 
            this.cmsFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFolderPathToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.resetFolderToolStripMenuItem});
            this.cmsFolder.Name = "cmsFolder";
            this.cmsFolder.Size = new System.Drawing.Size(152, 70);
            // 
            // changeFolderPathToolStripMenuItem
            // 
            this.changeFolderPathToolStripMenuItem.Name = "changeFolderPathToolStripMenuItem";
            this.changeFolderPathToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.changeFolderPathToolStripMenuItem.Text = "Change Folder";
            this.changeFolderPathToolStripMenuItem.Click += new System.EventHandler(this.changeFolderPathToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // resetFolderToolStripMenuItem
            // 
            this.resetFolderToolStripMenuItem.Name = "resetFolderToolStripMenuItem";
            this.resetFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.resetFolderToolStripMenuItem.Text = "Reset Folder";
            this.resetFolderToolStripMenuItem.Click += new System.EventHandler(this.resetFolderToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnMergeGenerator;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 551);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtOutPutFolder);
            this.Controls.Add(this.btnResetOptions);
            this.Controls.Add(this.btnGenerateScript);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnMergeGenerator);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pgOptions);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(928, 483);
            this.Name = "Form1";
            this.Text = "Generate Merge Statement";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.cmsFolder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PropertyGrid pgOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnMergeGenerator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnGenerateScript;
        private System.Windows.Forms.Button btnResetOptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOutPutFolder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsFolder;
        private System.Windows.Forms.ToolStripMenuItem changeFolderPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetFolderToolStripMenuItem;
    }
}

