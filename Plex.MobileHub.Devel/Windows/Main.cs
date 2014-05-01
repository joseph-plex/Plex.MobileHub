using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Plex.MobileHub.Devel.Views;
using Plex.MobileHub.Devel.Logic;
namespace Plex.MobileHub.Devel.Windows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            tabControl1.Dock = DockStyle.Fill;

            FormFactory f = new FormFactory();
            f.CreateApplicationSelection().ShowDialog(this); 

            //tabPage1.Controls.Add(new AppListSelect());
            //tabPage1.Controls.Add(new QueryPageControl());
            //tabPage2.Controls.Add(new TablePageControl());

        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Options o = new Options();
            Options p = new Options();
            p.MdiParent = this;
            o.MdiParent = this;

            o.Dock = DockStyle.Left;
            p.Dock = DockStyle.Fill;
            o.Show();
            p.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ActionForm(new SelectApplication()).ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevelManager.Instance.CloseProject();
        }
    }
}
