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
        const string OriginalName = "Plexxis Hub Client - ";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tabPage2.Controls.Add(new DatabaseView() { Dock = DockStyle.Fill });
            tabPage3.Controls.Add(new LogsView() { Dock = DockStyle.Fill });
            toggleConnectionMenuItems();
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
            manager.LogOn();

            toggleConnectionMenuItems();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = Manager.Instance;
            manager.LogOff();
            toggleConnectionMenuItems();
        }

        void toggleConnectionMenuItems()
        {
            if(Manager.Instance.IsConnected())
            {
                Text = OriginalName + "Connected";
                connectToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
            }
            else
            {
                Text = OriginalName + "Disconnected";
                connectToolStripMenuItem.Enabled = true;
                disconnectToolStripMenuItem.Enabled = false;
            }
        }
    }
}
