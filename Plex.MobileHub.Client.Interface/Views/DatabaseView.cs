using Plex.MobileHub.Client.Interface.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class DatabaseView : UserControl
    {
        public DatabaseView()
        {
            InitializeComponent();
            LoadToolStripComboBox();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            WinFactory winFactory = new WinFactory();
            Form win32 = winFactory.CreateDatabaseAdditionWindow();
            win32.ShowDialog(this);
        }

        void LoadToolStripComboBox()
        {
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();

            foreach (var ConnData in Manager.Instance.GetConnectionData())
                acsc.Add(ConnData.Company);

            toolStripComboBox1.ComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            toolStripComboBox1.ComboBox.AutoCompleteCustomSource = acsc;

        }
    }
}
