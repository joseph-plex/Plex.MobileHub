using Plex.MobileHub.Client.Interface.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Guifreaks.NavigationBar;
namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class DatabaseView : UserControl
    {
        const string ColumnSql = @"select t.COLUMN_NAME,t.DATA_TYPE, t.DATA_LENGTH,t.DATA_PRECISION, t.DATA_SCALE, t.NULLABLE  from ALL_TAB_COLUMNS t where table_name = :a";
        public DatabaseView()
        {
            InitializeComponent();
            Init();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            Action action = () => ReloadDatabase();
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        void ReloadDatabase()
        {
            WinFactory winFactory = new WinFactory();
            Form win32 = winFactory.CreateDatabaseAdditionWindow();
            win32.ShowDialog(this);
            Init();
        }


        private void naviBar1_Resize(object sender, EventArgs e)
        {
            var obj = ((NaviBar)sender);
            if (obj.Collapsed) {
                splitContainer2.SplitterDistance = obj.Size.Width;
                splitContainer2.IsSplitterFixed = true;
                splitContainer2.FixedPanel = FixedPanel.Panel1;
            }
            else {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterDistance = obj.Size.Width;
            }
        }

        public void Init()
        {
            var mgr = Manager.Instance;
            var DbInfo = mgr.CurrentDatabaseInformation();
            int value = new int();

            if (DbInfo != null)
                if(DbInfo.CompanyConnectionPairings != null)
                    value = DbInfo.CompanyConnectionPairings.Count();

            switch (value) {
                case 0 :
                    splitContainer2.Panel1Collapsed = true;
                    break;
                default:
                    naviBar1.SuspendLayout();
                    naviBar1.Bands.Clear();
                    foreach (var Info in DbInfo.CompanyConnectionPairings)
                    {
                        try
                        {
                            var tree = RetrieveCompanyTableTree(Info.Key);

                            NaviBand band = new NaviBand(components);
                            band.ClientArea.Location = new Point(0, 0);
                            band.ClientArea.Name = "ClientArea";
                            band.ClientArea.Size = new Size(208, 300);
                            //band.Location = new System.Drawing.Point(1, 27);
                            band.Name = "naviBand1";
                            band.Text = Info.Key;
                            //band.Size = new System.Drawing.Size(208, 300);
                            //band.TabIndex = 3;

                            tree.Dock = DockStyle.Fill;
                            band.ClientArea.Controls.Add(tree);
                            naviBar1.Controls.Add(band);
                            naviBar1.ActiveBand = band;

                            tree.NodeMouseClick += OnNodeDoubleClick; // show in table.
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show(e.ToString());
                            continue;
                        }
                    }
                    naviBar1.Collapsed = true;
                    naviBar1.ResumeLayout();

                    splitContainer2.Panel1Collapsed = (naviBar1.Bands.Count > 0)? false : true;
                    break;
            }
        }

        private void splitContainer2_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            naviBar1.Width = splitContainer2.SplitterDistance;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            naviBar1.Width = splitContainer2.SplitterDistance;
        }

        TreeView RetrieveCompanyTableTree(string code)
        {
            TreeView tree = new TreeView() { Dock = DockStyle.Fill };
            TreeNode tables = new TreeNode("Tables");
            List<DatabaseService.Row> tabs;

            try{
                tabs =  Manager.Instance.QuerySource(code, "select table_name from user_tables").Rows;
            }
            catch{
                tabs = new List<DatabaseService.Row>();
                tabs.Add(new DatabaseService.Row(){ Values = new List<object>(){ "hello"}});
            }

            foreach (var r in tabs)
                tables.Nodes.Add(new TreeNode(r.Values[0].ToString()));

            tree.Nodes.Add(tables);
            return tree;
        }

        void OnNodeDoubleClick(object sender , TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 1:
                    if(e.Node.Parent.Text == "Tables")
                    {
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                {
                                    DataTable t = new DataTable();
                                    var query = Manager.Instance.QuerySource(naviBar1.ActiveBand.Text, ColumnSql, new object[] { e.Node.Text });
                                    
                                    foreach(var col in query.Columns)
                                        t.Columns.Add(new DataColumn() { ColumnName = col.ColumnName });
                                    
                                    foreach(var row in query.Rows)
                                    {
                                        var dataRow = t.NewRow();
                                        for (int i = 0; i < query.Columns.Count; i++)
                                            dataRow[query.Columns[i].ColumnName] = row.Values[i];
                                        t.Rows.Add(dataRow);
                                    }
                                    dataGridView1.DataSource = t;
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        private void DatabaseView_Enter(object sender, EventArgs e)
        {
            OnMouseClick(new MouseEventArgs(MouseButtons.Left, 2, naviBar1.ActiveBand.Location.X + 2, naviBar1.ActiveBand.Location.Y + 2, 0));

        }
    }
}
