using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Devel.Logic;
using Plex.MobileHub.Devel.DevelService;
using Plex.MobileHub.Devel.Windows;


namespace Plex.MobileHub.Devel.Views
{

    //todo implement okay and cancel buttons
    //todo implement the conditional part of this
    public partial class CreateQueryControl : UserControl
    {
        public CreateQueryControl()
        {
            InitializeComponent();
            SizeChanged += CreateQueryControl_SizeChanged;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            LoadTables();
        }

        void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var cBox = (ComboBox)sender;
            var tabName = (string) cBox.SelectedValue;
            var tabs = DevelManager.Instance.AppTables;
            var tab  = tabs.FirstOrDefault(p => p.NAME == tabName);
            var Columns = DevelManager.Instance.AppTabColumns;
            ;

            listView1.Items.Clear();
            listView2.Items.Clear();
            foreach (var col in Columns.Where(p => p.TABLE_ID == tab.TABLE_ID))
                listView1.Items.Add(new ListViewItem() { Name = col.COLUMN_NAME, Text = col.COLUMN_NAME });
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems;
            foreach (ListViewItem item in items) 
            { 
                item.Remove();
                listView2.Items.Add(item);
            }
            listView2.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var items = listView2.SelectedItems;
            foreach (ListViewItem item in items)
            {
                item.Remove();
                listView1.Items.Add(item);
            
            }
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        void CreateQueryControl_SizeChanged(object sender, EventArgs e)
        {

        }

        void LoadTables()
        {
            var tabNames = new List<string>();
            foreach (var tab in DevelManager.Instance.AppTables)
                tabNames.Add(tab.NAME);
            comboBox1.DataSource = tabNames.ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QueryDefinition definition = new QueryDefinition();

            definition.QueryName = textBox1.Text;
            definition.TableName = (string)comboBox1.SelectedValue;
            definition.Description = textBox2.Text;
            var columnNames = new List<string>();

            foreach (ListViewItem item in listView2.Items)
                columnNames.Add(item.Text);

            definition.ColumnNames = columnNames.ToArray();
            definition.TrackDelta = checkBox1.Checked;

            //todo add whereclause
            definition.Sql = null;
            definition.WhereClauses = null;
            FormFactory factory = new FormFactory();
            factory.CreateLoadingForm(() => CreateQuery(definition)).Show();

        }

        void CreateQuery(QueryDefinition definition)
        { 
             DevelManager.Instance.CreateQuery(definition);
             DevelManager.Instance.RequestSync();
        }
    }
}
