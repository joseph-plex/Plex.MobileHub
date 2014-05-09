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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void AddDatabase_Load(object sender, EventArgs e)
        {
            try { 
                dataGridView1.DataSource = Manager.Instance.Discover();
            }
            catch(Exception x){
                MessageBox.Show(x.ToString());
            }
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

        void button1_Click(object sender, EventArgs e)
        {
            try { 
                Manager.Instance.RegisterDbConnection(textBox1.Text, textBox2.Text);
            }
            catch(Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }
    }
}
