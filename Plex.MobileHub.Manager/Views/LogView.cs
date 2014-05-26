using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Manager.Data;

namespace Plex.MobileHub.Manager.Views
{
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            LoadGridView();
        }

        void LoadGridView()
        {
            DataFactory factory = new DataFactory();
            dataGridView1.DataSource = factory.GetLogs().Reverse().ToArray();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
