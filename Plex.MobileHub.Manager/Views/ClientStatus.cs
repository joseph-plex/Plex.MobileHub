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
            LoadDataGrid();
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
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataFactory factory = new DataFactory();
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            switch (MessageBox.Show("This will Permanently delete the application from the system", "Are you sure?", MessageBoxButtons.YesNo))
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
    }
}
