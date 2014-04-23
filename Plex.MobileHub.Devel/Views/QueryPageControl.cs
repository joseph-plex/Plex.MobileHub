using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.Xml;

using Plex.MobileHub.Devel.DevelService;
using Plex.MobileHub.Devel.Logic;
using Plex.MobileHub.Devel.Windows;
namespace Plex.MobileHub.Devel.Views
{
    public partial class QueryPageControl : UserControl
    {
        //todo add the following buttons to toolbar  for this screen 
        //add query, delete query, link query, unlink query and refresh data
        const string QueryTreeHeader = "Queries";
        const string InfoTreeHeader = "Query Information";

        const string ColumnTreeHeader = "Query Columns";
        const string ConditionTreeHeader = "Query Conditions";
        public QueryPageControl()
        {
            InitializeComponent();

            splitContainer1.IsSplitterFixed = true;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;

            var Manager = DevelManager.Instance;

            Manager.RepositoryModified += Instance_NewProject;
            Manager.RepositoryModified += (s, e) => dataGridView1.InvokeIfRequired(() => dataGridView1.DataSource = null);
            Manager.RepositoryModified += (s, e) => toolStrip1.InvokeIfRequired(() => {foreach (ToolStripButton t in toolStrip1.Items) t.Enabled = Manager.DataLoaded;});
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;

        }


        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            try { 
                switch(node.Level)
                {
                    case 1:
                        {
                            //A query name has been selected
                            var queries = DevelManager.Instance.AppQueries;
                            PopulateGridView(queries.FirstOrDefault(p => p.NAME == node.Text));
                            break;
                        }
                    case 3:
                        //Either a Column or a Condition
                        {
                            var Parent = node.Parent;
                            if (Parent.Text == ColumnTreeHeader)
                            {
                                Parent = Parent.Parent; 
                                var query = DevelManager.Instance.AppQueries.FirstOrDefault(p => p.NAME == Parent.Text);
                                var qCol = DevelManager.Instance.GetAppQueryColumns(query.QUERY_ID).First(p => p.COLUMN_NAME == node.Text);
                                var Col = DevelManager.Instance.AppTabColumns.First(p => p.COLUMN_NAME == qCol.COLUMN_NAME);


                                new ActionForm(new AppTableColumnDetails(Col)).Show(this);
                                break;
                            }
                            if (Parent.Text == ConditionTreeHeader)
                                throw new NotImplementedException();
                            //todo implement stuff that is for conditions (QueryPageControl)
                            break;
                                //todo implement stuff that is for columns (QueryPageControl)
                        }
                     
                    default:
                        break;
                }
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        void Instance_NewProject(object sender, EventArgs e)
        {
            treeView1.InvokeIfRequired(()=> {
                var QueryInfo = DevelManager.Instance.AppQueries;
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add(GetQueryNodes(QueryInfo));
                }
            );
        }

        TreeNode GetQueryNodes(IEnumerable<APP_QUERIES> qrydefs)
        {
            TreeNode Base = new TreeNode(QueryTreeHeader);
            foreach (var def in qrydefs ?? new APP_QUERIES[0])
                Base.Nodes.Add(GetQueryNode(def));
            return Base;
        }

        TreeNode GetQueryNode(APP_QUERIES def)
        {
            TreeNode Base = new TreeNode(def.NAME);
            TreeNode Col = new TreeNode(ColumnTreeHeader);
            TreeNode Con = new TreeNode(ConditionTreeHeader);

            foreach (var c in DevelManager.Instance.GetAppQueryColumns(def.QUERY_ID))
                Col.Nodes.Add(c.COLUMN_NAME);
            foreach (var c in DevelManager.Instance.GetAppQueryConditions(def.QUERY_ID))
                Con.Nodes.Add(c.COLUMN_NAME);

            Base.Nodes.Add(Col);
            Base.Nodes.Add(Con);
            return Base;
        }

        void PopulateGridView(APP_QUERIES definition)
        {
            //todo show actual information from the app_tables view
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = DevelManager.Instance.GetAppQueryColumns(definition.QUERY_ID).ToArray();
            dataGridView1.ReadOnly = true;
        }

        private void QueryPageControl_ParentChanged(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new ActionForm(new CreateQueryControl()).ShowDialog(this);
        }
    }
}
