using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Client.Interface.Windows;
using Plex.MobileHub.Client.Interface.Views;
namespace Plex.MobileHub.Client.Interface
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tabPage2.Controls.Add(new DatabaseView() { Dock = DockStyle.Fill });
            tabPage3.Controls.Add(new LogsView() { Dock = DockStyle.Fill });
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFactory winFactory = new WinFactory();
            Form form = winFactory.GetLoginSettingWin32();
            form.ShowDialog(this);

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = Manager.Instance;
        }
    }
}
