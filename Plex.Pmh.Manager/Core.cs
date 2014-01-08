using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Plex.Pmh.Manager.ManagementResources;

namespace Plex.Pmh.Manager
{
    public partial class Core : Form
    {
        delegate void pfunc();
        public Core()
        {

            InitializeComponent();

            new Thread(() => SetClientView()).Start();
            new Thread(() => SetAppView()).Start();
            new Thread(() => SetLogView()).Start();
            x = this.Width;
            y = this.Height;
            
        }

        public void SetClientView()
        {
            //todo put a refresh for Set Client View on refresh
            var x = WebService.AppsGet();
            pfunc m = () => { clientDpInfoBindingSource.Clear(); foreach (var o in x) clientDpInfoBindingSource.Add(o); };
            if (InvokeRequired)
                Invoke(m);
            else
                m();
        }

        public void SetAppView()
        {
            //todo put a refresh signal somewhere for Set App View on during
            var x = WebService.AppsGet();
            pfunc m = () => { appsBindingSource1.Clear(); foreach (var a in x) appsBindingSource1.Add(a); };
            if (InvokeRequired)
                Invoke(m);
            else
                m();
        }

        public void SetLogView()
        {
            //todo put a refresh for SetLog For
            var x = WebService.LogsGet();
            pfunc m = () => { logsBindingSource.Clear(); foreach (var l in x) logsBindingSource.Add(l); };
            if (InvokeRequired)
                Invoke(m);
            else
                m();
        }


        private void ClientCreate_Click(object sender, EventArgs e)
        {
            //init
            ClientAddForm caf = new ClientAddForm();
            Clients newClient = new Clients();

            //get information from the end user
            switch(caf.ShowDialog(this))
            {
                case DialogResult.OK:
                    newClient.Description = caf.Description;
                    newClient.ClientKey = caf.ClientKey;

                    WebService.ClientCreate(newClient);
                    new Thread(() => SetClientView()).Start();
                    break;
                default:
                    break;  
            }
        }

        private void ClientDelete_Click(object sender, EventArgs e)
        {
            string confirmText = "Are you sure you wish to delete this client?";
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            string Caption = "Are you Sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //this should only have one selected row becuase multiselect on grid is false
            foreach (DataGridViewRow r in ClientGridView.SelectedRows)
                if (MessageBox.Show(confirmText, Caption, buttons,icon) == DialogResult.Yes)
                {
                    Clients deleteableClient = new Clients();
                    deleteableClient.ClientId = Convert.ToInt32(r.Cells["clientIdDataGridViewTextBoxColumn"].Value);
                    WebService.ClientDelete(deleteableClient);
                    new Thread(() => SetClientView()).Start();
                }
            
        }


        private void AppCreate_Click(object sender, EventArgs e)
        {
            AppAddForm aaf = new AppAddForm();
            Apps newApp = new Apps();
            //todo logic to ensure two app id's of the same type don't exist.
            switch (aaf.ShowDialog(this))
            {
                case DialogResult.OK:
                    MessageBox.Show("This is your case");
                    newApp.AppId = aaf.AppId;
                    newApp.AuthKey = aaf.AuthKey;
                    newApp.Description = aaf.Description;
                    newApp.Title = aaf.Title;
                    newApp.isClientCustomApp = aaf.IsPrivate;

                    WebService.AppCreate(newApp);
                    new Thread(() => SetAppView()).Start();

                    break;
                default:
                    break;
            }
        }

        private void AppDelete_Click(object sender, EventArgs e)
        {
            string confirmText = "Are you sure you wish to delete this Application?";
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            string Caption = "Are you Sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //this should only have one selected row becuase multiselect on grid is false
            foreach (DataGridViewRow r in AppDataView.SelectedRows)
                if (MessageBox.Show(confirmText, Caption, buttons, icon) == DialogResult.Yes)
                {

                    
                    Apps app = new Apps();
                    app.AppId = Convert.ToInt32(r.Cells["appIdDataGridViewTextBoxColumn"].Value);

                    WebService.AppDelete(app);
                    new Thread(() => SetAppView()).Start();
                }
            
        }

        private int x;
        private int y;


        private void Core_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Core_Paint(object sender, PaintEventArgs e)
        {
            Control con = (Control)sender;
            Navi.Width += con.Width - x;
            Navi.Height += con.Height - y;

            x = this.Width;
            y = this.Height;
        }

        private void ClientGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
