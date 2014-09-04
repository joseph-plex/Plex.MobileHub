using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pmh.Manager.Data;

namespace Pmh.Manager.Views
{
    public partial class ConnectionView : UserControl
    {
        public ConnectionView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            LoadData();
        }

        public void LoadData()
        {
            DataFactory factory = new DataFactory();
            consumerBindingSource.Clear();
            foreach (var consumer in factory.GetConsumer())
                consumerBindingSource.Add(consumer);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                toolStripButton1.Enabled = false;
            else
                toolStripButton1.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new NotImplementedException().Message);
        }
    }
}
