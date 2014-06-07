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
            if(!Manager.Instance.ValidateClientSettings())
                accountSettingsToolStripMenuItem_Click(this, EventArgs.Empty);
            init();
        }
          

        void init()
        {
            SuspendLayout();
            tabPage1.Controls.Clear();
            tabPage1.Controls.Add(new UsersView() { Dock = DockStyle.Fill });
            tabPage2.Controls.Clear();
            tabPage2.Controls.Add(new DatabaseView() { Dock = DockStyle.Fill });
            tabPage3.Controls.Clear();
            tabPage3.Controls.Add(new LogsView() { Dock = DockStyle.Fill });
            toggleConnectionMenuItems();
            ResumeLayout();

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

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFactory factory = new WinFactory();
            Manager mgr = Manager.Instance;

            var WasValid = mgr.ValidateClientSettings();
            var key = mgr.ClientKey;
            var Id = mgr.ClientId;
            var OmitError = true;

            do
            {

                if (!OmitError)
                    MessageBox.Show("Invalid Client Credentials, Please try again");

                switch (factory.GetAccountSettingsWin32().ShowDialog())
                {
                    case DialogResult.OK:
                        OmitError = mgr.ValidateClientSettings();
                        break;
                    default:
                        if (WasValid) { 
                            mgr.ClientKey = key;
                            mgr.ClientId = Id;
                            return;
                        }
                        else
                        {
                            Close();
                        }
                        break;
                }
            } while (!OmitError);
            init();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
