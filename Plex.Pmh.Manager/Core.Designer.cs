namespace Plex.Pmh.Manager
{
    partial class Core
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
            this.Navi = new System.Windows.Forms.TabControl();
            this.ClientsTab = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ViewDBUserData = new System.Windows.Forms.Button();
            this.ClientDelete = new System.Windows.Forms.Button();
            this.ClientCreate = new System.Windows.Forms.Button();
            this.ClientGridView = new System.Windows.Forms.DataGridView();
            this.onlineDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPv4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onlineDurationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientDpInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AppsTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ViewAppData = new System.Windows.Forms.Button();
            this.SchemaEdit = new System.Windows.Forms.Button();
            this.AppDelete = new System.Windows.Forms.Button();
            this.AppCreate = new System.Windows.Forms.Button();
            this.AppDataView = new System.Windows.Forms.DataGridView();
            this.authKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isClientCustomAppDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.appsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.LogsTab = new System.Windows.Forms.TabPage();
            this.LogsGridView = new System.Windows.Forms.DataGridView();
            this.logIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Navi.SuspendLayout();
            this.ClientsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientDpInfoBindingSource)).BeginInit();
            this.AppsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AppDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource1)).BeginInit();
            this.LogsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Navi
            // 
            this.Navi.Controls.Add(this.ClientsTab);
            this.Navi.Controls.Add(this.AppsTab);
            this.Navi.Controls.Add(this.LogsTab);
            this.Navi.Location = new System.Drawing.Point(13, 12);
            this.Navi.Name = "Navi";
            this.Navi.SelectedIndex = 0;
            this.Navi.Size = new System.Drawing.Size(1136, 560);
            this.Navi.TabIndex = 0;
            // 
            // ClientsTab
            // 
            this.ClientsTab.Controls.Add(this.splitContainer2);
            this.ClientsTab.Location = new System.Drawing.Point(4, 22);
            this.ClientsTab.Name = "ClientsTab";
            this.ClientsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ClientsTab.Size = new System.Drawing.Size(1128, 534);
            this.ClientsTab.TabIndex = 0;
            this.ClientsTab.Text = "Clients";
            this.ClientsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ViewDBUserData);
            this.splitContainer2.Panel1.Controls.Add(this.ClientDelete);
            this.splitContainer2.Panel1.Controls.Add(this.ClientCreate);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ClientGridView);
            this.splitContainer2.Size = new System.Drawing.Size(1122, 528);
            this.splitContainer2.SplitterDistance = 29;
            this.splitContainer2.TabIndex = 0;
            // 
            // ViewDBUserData
            // 
            this.ViewDBUserData.Location = new System.Drawing.Point(65, 3);
            this.ViewDBUserData.Name = "ViewDBUserData";
            this.ViewDBUserData.Size = new System.Drawing.Size(145, 23);
            this.ViewDBUserData.TabIndex = 4;
            this.ViewDBUserData.Text = "View Users/Databases";
            this.ViewDBUserData.UseVisualStyleBackColor = true;
            // 
            // ClientDelete
            // 
            this.ClientDelete.Location = new System.Drawing.Point(33, 3);
            this.ClientDelete.Name = "ClientDelete";
            this.ClientDelete.Size = new System.Drawing.Size(26, 23);
            this.ClientDelete.TabIndex = 1;
            this.ClientDelete.Text = "-";
            this.ClientDelete.UseVisualStyleBackColor = true;
            this.ClientDelete.Click += new System.EventHandler(this.ClientDelete_Click);
            // 
            // ClientCreate
            // 
            this.ClientCreate.AutoEllipsis = true;
            this.ClientCreate.Location = new System.Drawing.Point(3, 3);
            this.ClientCreate.Name = "ClientCreate";
            this.ClientCreate.Size = new System.Drawing.Size(24, 23);
            this.ClientCreate.TabIndex = 0;
            this.ClientCreate.Text = "+";
            this.ClientCreate.UseVisualStyleBackColor = true;
            this.ClientCreate.Click += new System.EventHandler(this.ClientCreate_Click);
            // 
            // ClientGridView
            // 
            this.ClientGridView.AllowUserToAddRows = false;
            this.ClientGridView.AllowUserToDeleteRows = false;
            this.ClientGridView.AllowUserToOrderColumns = true;
            this.ClientGridView.AutoGenerateColumns = false;
            this.ClientGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.onlineDataGridViewCheckBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.clientIdDataGridViewTextBoxColumn,
            this.iPv4DataGridViewTextBoxColumn,
            this.connectedDataGridViewTextBoxColumn,
            this.onlineDurationDataGridViewTextBoxColumn});
            this.ClientGridView.DataSource = this.clientDpInfoBindingSource;
            this.ClientGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientGridView.Location = new System.Drawing.Point(0, 0);
            this.ClientGridView.MultiSelect = false;
            this.ClientGridView.Name = "ClientGridView";
            this.ClientGridView.ReadOnly = true;
            this.ClientGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ClientGridView.Size = new System.Drawing.Size(1122, 495);
            this.ClientGridView.TabIndex = 0;
            this.ClientGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientGridView_CellContentClick);
            // 
            // onlineDataGridViewCheckBoxColumn
            // 
            this.onlineDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.onlineDataGridViewCheckBoxColumn.DataPropertyName = "Online";
            this.onlineDataGridViewCheckBoxColumn.HeaderText = "Online";
            this.onlineDataGridViewCheckBoxColumn.Name = "onlineDataGridViewCheckBoxColumn";
            this.onlineDataGridViewCheckBoxColumn.ReadOnly = true;
            this.onlineDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.onlineDataGridViewCheckBoxColumn.Width = 62;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clientIdDataGridViewTextBoxColumn
            // 
            this.clientIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.clientIdDataGridViewTextBoxColumn.DataPropertyName = "ClientId";
            this.clientIdDataGridViewTextBoxColumn.HeaderText = "ClientId";
            this.clientIdDataGridViewTextBoxColumn.Name = "clientIdDataGridViewTextBoxColumn";
            this.clientIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.clientIdDataGridViewTextBoxColumn.Width = 67;
            // 
            // iPv4DataGridViewTextBoxColumn
            // 
            this.iPv4DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iPv4DataGridViewTextBoxColumn.DataPropertyName = "IPv4";
            this.iPv4DataGridViewTextBoxColumn.HeaderText = "IPv4";
            this.iPv4DataGridViewTextBoxColumn.Name = "iPv4DataGridViewTextBoxColumn";
            this.iPv4DataGridViewTextBoxColumn.ReadOnly = true;
            this.iPv4DataGridViewTextBoxColumn.Width = 54;
            // 
            // connectedDataGridViewTextBoxColumn
            // 
            this.connectedDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.connectedDataGridViewTextBoxColumn.DataPropertyName = "Connected";
            this.connectedDataGridViewTextBoxColumn.HeaderText = "Connected";
            this.connectedDataGridViewTextBoxColumn.Name = "connectedDataGridViewTextBoxColumn";
            this.connectedDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectedDataGridViewTextBoxColumn.Width = 84;
            // 
            // onlineDurationDataGridViewTextBoxColumn
            // 
            this.onlineDurationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.onlineDurationDataGridViewTextBoxColumn.DataPropertyName = "OnlineDuration";
            this.onlineDurationDataGridViewTextBoxColumn.HeaderText = "OnlineDuration";
            this.onlineDurationDataGridViewTextBoxColumn.Name = "onlineDurationDataGridViewTextBoxColumn";
            this.onlineDurationDataGridViewTextBoxColumn.ReadOnly = true;
            this.onlineDurationDataGridViewTextBoxColumn.Width = 102;
            // 
            // clientDpInfoBindingSource
            // 
            this.clientDpInfoBindingSource.DataSource = typeof(Plex.Pmh.Manager.ManagementResources.Apps);
            // 
            // AppsTab
            // 
            this.AppsTab.Controls.Add(this.splitContainer1);
            this.AppsTab.Location = new System.Drawing.Point(4, 22);
            this.AppsTab.Name = "AppsTab";
            this.AppsTab.Padding = new System.Windows.Forms.Padding(3);
            this.AppsTab.Size = new System.Drawing.Size(1128, 534);
            this.AppsTab.TabIndex = 1;
            this.AppsTab.Text = "Apps";
            this.AppsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ViewAppData);
            this.splitContainer1.Panel1.Controls.Add(this.SchemaEdit);
            this.splitContainer1.Panel1.Controls.Add(this.AppDelete);
            this.splitContainer1.Panel1.Controls.Add(this.AppCreate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AppDataView);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 528);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 1;
            // 
            // ViewAppData
            // 
            this.ViewAppData.Location = new System.Drawing.Point(148, 3);
            this.ViewAppData.Name = "ViewAppData";
            this.ViewAppData.Size = new System.Drawing.Size(69, 23);
            this.ViewAppData.TabIndex = 5;
            this.ViewAppData.Text = "View App Data";
            this.ViewAppData.UseVisualStyleBackColor = true;
            // 
            // SchemaEdit
            // 
            this.SchemaEdit.Location = new System.Drawing.Point(67, 3);
            this.SchemaEdit.Name = "SchemaEdit";
            this.SchemaEdit.Size = new System.Drawing.Size(75, 23);
            this.SchemaEdit.TabIndex = 4;
            this.SchemaEdit.Text = "Edit Schema";
            this.SchemaEdit.UseVisualStyleBackColor = true;
            // 
            // AppDelete
            // 
            this.AppDelete.Location = new System.Drawing.Point(36, 3);
            this.AppDelete.Name = "AppDelete";
            this.AppDelete.Size = new System.Drawing.Size(25, 23);
            this.AppDelete.TabIndex = 2;
            this.AppDelete.Text = "-";
            this.AppDelete.UseVisualStyleBackColor = true;
            this.AppDelete.Click += new System.EventHandler(this.AppDelete_Click);
            // 
            // AppCreate
            // 
            this.AppCreate.Location = new System.Drawing.Point(3, 3);
            this.AppCreate.Name = "AppCreate";
            this.AppCreate.Size = new System.Drawing.Size(27, 23);
            this.AppCreate.TabIndex = 0;
            this.AppCreate.Text = "+";
            this.AppCreate.UseVisualStyleBackColor = true;
            this.AppCreate.Click += new System.EventHandler(this.AppCreate_Click);
            // 
            // AppDataView
            // 
            this.AppDataView.AllowUserToAddRows = false;
            this.AppDataView.AllowUserToDeleteRows = false;
            this.AppDataView.AutoGenerateColumns = false;
            this.AppDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.authKeyDataGridViewTextBoxColumn,
            this.appIdDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn1,
            this.isClientCustomAppDataGridViewCheckBoxColumn});
            this.AppDataView.DataSource = this.appsBindingSource1;
            this.AppDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppDataView.Location = new System.Drawing.Point(0, 0);
            this.AppDataView.MultiSelect = false;
            this.AppDataView.Name = "AppDataView";
            this.AppDataView.ReadOnly = true;
            this.AppDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AppDataView.Size = new System.Drawing.Size(1122, 498);
            this.AppDataView.TabIndex = 0;
            // 
            // authKeyDataGridViewTextBoxColumn
            // 
            this.authKeyDataGridViewTextBoxColumn.DataPropertyName = "AuthKey";
            this.authKeyDataGridViewTextBoxColumn.HeaderText = "AuthKey";
            this.authKeyDataGridViewTextBoxColumn.Name = "authKeyDataGridViewTextBoxColumn";
            this.authKeyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // appIdDataGridViewTextBoxColumn
            // 
            this.appIdDataGridViewTextBoxColumn.DataPropertyName = "AppId";
            this.appIdDataGridViewTextBoxColumn.HeaderText = "AppId";
            this.appIdDataGridViewTextBoxColumn.Name = "appIdDataGridViewTextBoxColumn";
            this.appIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            this.descriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // isClientCustomAppDataGridViewCheckBoxColumn
            // 
            this.isClientCustomAppDataGridViewCheckBoxColumn.DataPropertyName = "isClientCustomApp";
            this.isClientCustomAppDataGridViewCheckBoxColumn.HeaderText = "isClientCustomApp";
            this.isClientCustomAppDataGridViewCheckBoxColumn.Name = "isClientCustomAppDataGridViewCheckBoxColumn";
            this.isClientCustomAppDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // appsBindingSource1
            // 
            this.appsBindingSource1.DataSource = typeof(Plex.Pmh.Manager.ManagementResources.Apps);
            // 
            // LogsTab
            // 
            this.LogsTab.Controls.Add(this.LogsGridView);
            this.LogsTab.Location = new System.Drawing.Point(4, 22);
            this.LogsTab.Name = "LogsTab";
            this.LogsTab.Padding = new System.Windows.Forms.Padding(3);
            this.LogsTab.Size = new System.Drawing.Size(1128, 534);
            this.LogsTab.TabIndex = 2;
            this.LogsTab.Text = "Logs";
            this.LogsTab.UseVisualStyleBackColor = true;
            // 
            // LogsGridView
            // 
            this.LogsGridView.AllowUserToAddRows = false;
            this.LogsGridView.AllowUserToDeleteRows = false;
            this.LogsGridView.AllowUserToOrderColumns = true;
            this.LogsGridView.AutoGenerateColumns = false;
            this.LogsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logIdDataGridViewTextBoxColumn,
            this.logDateDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.LogsGridView.DataSource = this.logsBindingSource;
            this.LogsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogsGridView.Location = new System.Drawing.Point(3, 3);
            this.LogsGridView.MultiSelect = false;
            this.LogsGridView.Name = "LogsGridView";
            this.LogsGridView.ReadOnly = true;
            this.LogsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogsGridView.Size = new System.Drawing.Size(1122, 528);
            this.LogsGridView.TabIndex = 0;
            this.LogsGridView.Tag = "test";
            // 
            // logIdDataGridViewTextBoxColumn
            // 
            this.logIdDataGridViewTextBoxColumn.DataPropertyName = "LogId";
            this.logIdDataGridViewTextBoxColumn.HeaderText = "LogId";
            this.logIdDataGridViewTextBoxColumn.Name = "logIdDataGridViewTextBoxColumn";
            this.logIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // logDateDataGridViewTextBoxColumn
            // 
            this.logDateDataGridViewTextBoxColumn.DataPropertyName = "LogDate";
            this.logDateDataGridViewTextBoxColumn.HeaderText = "LogDate";
            this.logDateDataGridViewTextBoxColumn.Name = "logDateDataGridViewTextBoxColumn";
            this.logDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // logsBindingSource
            // 
            this.logsBindingSource.AllowNew = false;
            this.logsBindingSource.DataSource = typeof(Plex.Pmh.Manager.ManagementResources.Logs);
            // 
            // Core
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 584);
            this.Controls.Add(this.Navi);
            this.Name = "Core";
            this.Text = "Core";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Core_Paint);
            this.Resize += new System.EventHandler(this.Core_Resize);
            this.Navi.ResumeLayout(false);
            this.ClientsTab.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClientGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientDpInfoBindingSource)).EndInit();
            this.AppsTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AppDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource1)).EndInit();
            this.LogsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Navi;
        private System.Windows.Forms.TabPage ClientsTab;
        private System.Windows.Forms.TabPage AppsTab;
        private System.Windows.Forms.TabPage LogsTab;
        private System.Windows.Forms.DataGridView LogsGridView;
        private System.Windows.Forms.BindingSource logsBindingSource;
        private System.Windows.Forms.DataGridView AppDataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn logIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn logDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.BindingSource appsBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn authKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn appIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isClientCustomAppDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridView ClientGridView;
        private System.Windows.Forms.BindingSource clientDpInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn onlineDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPv4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onlineDurationDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button ClientDelete;
        private System.Windows.Forms.Button ClientCreate;
        private System.Windows.Forms.Button AppDelete;
        private System.Windows.Forms.Button AppCreate;
        private System.Windows.Forms.Button ViewDBUserData;
        private System.Windows.Forms.Button ViewAppData;
        private System.Windows.Forms.Button SchemaEdit;
    }
}