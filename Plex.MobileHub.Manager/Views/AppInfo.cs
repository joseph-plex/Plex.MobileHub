using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Guifreaks.NavigationBar;
using Plex.MobileHub.Manager.Data;

namespace Plex.MobileHub.Manager.Views
{
    using Tuple = Plex.MobileHub.Manager.Data.Tuple;
    public partial class AppInfo : UserControl
    {
        public AppInfo()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            dataGridView3.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Access", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView3.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Client Id", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Column Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Column Sequence", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Data Type", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn() { Name = "table Column Id", Visible = false });
            
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Table Name" , AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill});
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Table Id ", Visible = false });

            LoadlistBox();
        }

        void LoadlistBox()
        {
            SuspendLayout();
            listBox1.Items.Clear();

            DataFactory factory = new DataFactory();
            var queryResult = factory.Query("select * from apps");
            var appTitles = queryResult["Title"].Cast<String>();

            listBox1.DataSource = appTitles.ToList();
            ResumeLayout();
        }
        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                ListBox lbox = ((ListBox)sender);

                if (lbox.SelectedValue == null)
                {
                    ToggleToolStripButtons(false);
                    return;
                }

                DataFactory factory = new DataFactory();
                var queryResult = factory.Query("select * from apps");

                LoadGeneralInformation(queryResult, lbox.SelectedValue.ToString());

                var row = queryResult.Rows.First(p => p[queryResult.GetColumnIndex("Title")].Equals(lbox.SelectedValue.ToString()));
                var appId = Convert.ToInt32(row[queryResult.GetColumnIndex("app_id")]);
                LoadClientAppGrid(appId);

                LoadAppGridViewInformation(appId);

                ToggleToolStripButtons(true);
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        void LoadGeneralInformation(Result queryResult, String Title)
        {
            var titleIndex = queryResult.GetColumnIndex("Title");
            var row = queryResult.Rows.First(p => p[titleIndex].Equals(Title));

            int AppId = Convert.ToInt32(row[queryResult.GetColumnIndex("app_id")]);
            string AppKey = (string)row[queryResult.GetColumnIndex("auth_key")];
            string AppTitle = (string)row[queryResult.GetColumnIndex("title")];
            string Description = (string)row[queryResult.GetColumnIndex("description")];
            bool IsCustom = Convert.ToBoolean(row[queryResult.GetColumnIndex("is_client_custom_app")]);

            textBox1.Text = AppId.ToString();
            textBox2.Text = AppKey.ToString();
            textBox3.Text = AppTitle.ToString();
            textBox4.Text = IsCustom.ToString();
            textBox5.Text = Description.ToString();
        }

        void LoadClientAppGrid(int AppId)
        {
            DataFactory factory = new DataFactory();
            List<object> ClientAppList = new List<object>();
            Result clients =  factory.Query("select * from clients");
            Result clientApps = factory.Query("Select * from client_apps");

            int clientIdIndex = clientApps.GetColumnIndex("CLIENT_ID");
            int appIdIndex = clientApps.GetColumnIndex("APP_ID");

            dataGridView3.Rows.Clear();
            foreach(var row in clients["CLIENT_ID"])
            {
                int r = Convert.ToInt32(row);
                bool exists = clientApps.Rows.Any(p => Convert.ToInt32(p[clientIdIndex]) == r && Convert.ToInt32(p[appIdIndex]) == AppId);

                var index = dataGridView3.Rows.Add();
                dataGridView3.Rows[index].Cells[1].Value = r;
                dataGridView3.Rows[index].Cells[0].Value = exists;
            }
        }

        void ToggleToolStripButtons(bool value)
        {
            toolStripButton3.Enabled = value;
            toolStripButton4.Enabled = value;
            toolStripButton5.Enabled = value;
            toolStripButton6.Enabled = value;
            toolStripButton7.Enabled = value;
            toolStripButton8.Enabled = value;
        }

        void LoadAppGridViewInformation(int AppId)
        {
            DataFactory factory = new DataFactory();
            var queryResult = factory.Query("select * from app_tables");
            var tableNameIndex = queryResult.GetColumnIndex("NAME");
            var appIdIndex = queryResult.GetColumnIndex("APP_ID");
            var tableIdIndex = queryResult.GetColumnIndex("TABLE_ID");

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            foreach (var r in queryResult.Rows)
            {
                int id = Convert.ToInt32(r[appIdIndex]);
                string name = Convert.ToString(r[tableNameIndex]);
                int table = Convert.ToInt32(r[tableIdIndex]);

                if (id == AppId)
                    dataGridView1.Rows.Add(name,table);
            }
        }

        void naviBar1_Resize(object sender, EventArgs e)
        {
            var obj = ((NaviBar)sender);
            if(obj.Collapsed)
            {
                splitContainer3.SplitterDistance = obj.Size.Width;
                splitContainer3.IsSplitterFixed = true;
            }
            else
            {
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterDistance = obj.Size.Width;
            }
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            var selectedrow = dataGridView1.SelectedRows[0];
            var name = selectedrow.Cells[0].Value;
            var id = Convert.ToInt32(selectedrow.Cells[1].Value);


            DataFactory factory = new DataFactory();
            var queryResult = factory.Query("select * from app_table_columns");

            var tableColumnIdIndex = queryResult.GetColumnIndex("table_column_id");
            var columnSeqenceIndex = queryResult.GetColumnIndex("Column_sequence");
            var columnNameIndex = queryResult.GetColumnIndex("Column_name");
            var dataTypeIndex = queryResult.GetColumnIndex("data_type");
            var tableIdIndex = queryResult.GetColumnIndex("table_id");

            dataGridView2.Rows.Clear();
            foreach (var row in queryResult.Rows)
            {
                int TableId = Convert.ToInt32(row[tableIdIndex]);
                if (TableId == id)
                {
                    dataGridView2.Rows.Add(
                        Convert.ToString(row[columnNameIndex]),
                        Convert.ToInt32(row[columnSeqenceIndex]),
                        Convert.ToString(row[dataTypeIndex]),
                        Convert.ToInt32(row[tableColumnIdIndex])
                    );
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //Delete an app table column
            throw new NotImplementedException();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
