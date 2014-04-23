using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plex.MobileHub.Devel.DevelService;
using Plex.MobileHub.Devel.Logic;

namespace Plex.MobileHub.Devel.Views
{
    public partial class TablePageControl : UserControl
    {
        public TablePageControl()
        {
            InitializeComponent();

            ParentChanged += (s, e) => Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer2.Dock = DockStyle.Fill;
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
            DevelManager.Instance.RepositoryModified += Instance_RepositoryModified;
        }

        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            this.InvokeIfRequired(() => LoadGrid(node.Text));
        }

        void Instance_RepositoryModified(object sender, EventArgs e)
        {
            this.InvokeIfRequired(() => LoadTables());
        }

        void LoadTables()
        {
            var TableCollection = DevelManager.Instance.AppTables;
            treeView1.Nodes.Clear();
            dataGridView1.DataSource = null;
            var Base = new TreeNode("Tables");
            treeView1.Nodes.Add(Base);

            foreach (var Table in TableCollection ?? new APP_TABLES[0])
                Base.Nodes.Add(new TreeNode(Table.NAME));
        }

        void LoadGrid(string TableName)
        {
            var table = DevelManager.Instance.AppTables.First(p => p.NAME == TableName);
            dataGridView1.DataSource = DevelManager.Instance.AppTabColumns.Where(p => p.TABLE_ID == table.TABLE_ID).ToArray();
        }
    }
}
