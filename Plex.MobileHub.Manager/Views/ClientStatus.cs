using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Manager.Data;
using Plex.MobileHub.Manager.Windows;

namespace Plex.MobileHub.Manager.Views
{
    public partial class ClientStatus : UserControl
    {
        public ClientStatus()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            dataGridView2.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Access", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "App Id", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "App Title", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            LoadDataGrid();
        }

        void LoadClientAppGrid(int clientId)
        {
            DataFactory factory = new DataFactory();
            List<object> ClientAppList = new List<object>();

            Result apps = factory.Query("select * from APPS");
            Result clientApps = factory.Query("Select * from client_apps where client_id = :a", clientId);


            int Index = apps.GetColumnIndex("APP_ID");
            int TitleIndex = apps.GetColumnIndex("TITLE");
            int appIdIndex = clientApps.GetColumnIndex("APP_ID");

            dataGridView2.Rows.Clear();

            foreach (var row in apps.Rows)
            {
                int appId = Convert.ToInt32(row[Index]);
                bool exists = clientApps.Rows.Any(p => Convert.ToInt32(p[appIdIndex]) == appId);
                dataGridView2.Rows.Add(exists, appId, row[TitleIndex].ToString());
            }
        }

        void LoadDataGrid()
        {
            DataFactory factory = new DataFactory();
            var queryResult = factory.Query("select * from clients");

            var ipIndex = queryResult.GetColumnIndex("CLIENT_IP_ADDRESS");
            var clientIdIndex = queryResult.GetColumnIndex("CLIENT_ID");
            var clientKeyIndex = queryResult.GetColumnIndex("CLIENT_KEY");
            var clientPortIndex = queryResult.GetColumnIndex("CLIENT_PORT");
            var descriptionIndex = queryResult.GetColumnIndex("DESCRIPTION");

            dataGridView1.Rows.Clear();
            foreach(var row in queryResult.Rows)
            {
                List<Object> rowValues = new List<Object>();
                rowValues.Add((Convert.ToInt32(row[clientPortIndex]) != 0) ? true : false);
                rowValues.Add(Convert.ToInt32(row[clientIdIndex]));
                rowValues.Add(Convert.ToString(row[clientKeyIndex]));
                rowValues.Add(Convert.ToString(row[descriptionIndex]));
                rowValues.Add(Convert.ToString(row[ipIndex]));
                rowValues.Add(Convert.ToInt32(row[clientPortIndex]));

                dataGridView1.Rows.Add(rowValues.ToArray());
            }
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
            }
            else
            {
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = true;
                LoadClientAppGrid(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataFactory factory = new DataFactory();
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            switch (MessageBox.Show("This will immediately and permanently delete the application from the system", "Are you sure?", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    var queryResult = factory.NonQuery("delete from clients where client_id = :a", row.Cells[1].Value);
                    LoadDataGrid();
                    break;
                case DialogResult.No :
                default:
                    return;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            switch(new ClientCreate().ShowDialog(this))
            {
                case DialogResult.OK:
                    LoadDataGrid();
                    break;
                default:
                    break;
            }
        }

  
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            if (tableLayoutPanel1.RowStyles[0].SizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
            else
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            ManagePanels();
            ResumeLayout();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SuspendLayout();

            if (tableLayoutPanel1.RowStyles[1].SizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Absolute;
            else
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            ManagePanels();
            ResumeLayout();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            if (tableLayoutPanel1.RowStyles[2].SizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute;
            else
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
            ManagePanels();
            ResumeLayout();
        }

        void ManagePanels()
        {
            var accessSizeType = tableLayoutPanel1.RowStyles[0].SizeType;
            var clientSizeType = tableLayoutPanel1.RowStyles[1].SizeType;
            var logsSizeType = tableLayoutPanel1.RowStyles[2].SizeType;
            int count = new int();

            if (accessSizeType == SizeType.Percent)
                count++;
            else
                tableLayoutPanel1.RowStyles[0].Height = 0;

            if (clientSizeType == SizeType.Percent)
                count++;
            else
                tableLayoutPanel1.RowStyles[1].Height = 0;

            if (logsSizeType == SizeType.Percent)
                count++;
            else
                tableLayoutPanel1.RowStyles[2].Height = 0;

            if (count == 0){
                splitContainer1.Panel2Collapsed = true;
                return;
            }
            else
                splitContainer1.Panel2Collapsed = false;
           
            float newHeight = (tableLayoutPanel1.Height / count);

            if (accessSizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[0].Height = Convert.ToInt32(newHeight);

            if (clientSizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[1].Height = Convert.ToInt32(newHeight);

            if (logsSizeType == SizeType.Percent)
                tableLayoutPanel1.RowStyles[2].Height = Convert.ToInt32(newHeight);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
    }
}
