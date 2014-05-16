using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plex.MobileHub.Client.Interface.DatabaseService;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class AddDatabase : UserControl
    {
        public AddDatabase()
        {
            InitializeComponent();
            LoadInformation();
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var grid = (DataGridView)sender;
            var rowCollection = new List<DataGridViewRow>();

            foreach(DataGridViewRow row in grid.SelectedRows)
                rowCollection.Add(row);

            if (rowCollection.Count == 0)
                return;

            if(rowCollection.Count > 1){
                grid.ClearSelection();
                throw new InvalidOperationException("Only one row can be selected at any given time");
            }

            var selectedRow = rowCollection.First();
            textBox1.Text = Convert.ToString(selectedRow.Cells[0].Value);
            textBox2.Text = Convert.ToString(selectedRow.Cells[1].Value);
        }

        void LoadInformation()
        {
            try {
                List<object> datasource = new List<object>();
                var DbInfo = Manager.Instance.CurrentDatabaseInformation();
              
                if(DbInfo == null)
                    return;
                if(DbInfo.CompanyConnectionPairings == null)
                    return;

                foreach (var pair in DbInfo.CompanyConnectionPairings)
                    datasource.Add(new { CompanyCode = pair.Key, ConnectionString = pair.Value });

                dataGridView1.DataSource = datasource;

                switch (dataGridView1.Columns.Count)
                {
                    case 0 :
                        return;
                    default:
                        var col = dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None);
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        break;
                }
            }
            catch (Exception x) {
                MessageBox.Show(x.ToString());
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            try {
                Manager.Instance.RegisterDbConnectionData(textBox1.Text, textBox2.Text);
            }
            catch(Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }
    }
}
