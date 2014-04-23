using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Plex.MobileHub.Devel.Windows
{
    public partial class Options : Form
    {
        const int NameIndex = 0;
        const int TypeIndex = 1;
        const int ValueIndex = 2;
        public Options()
        {
            InitializeComponent();
            RefreshGrid();
        }
     

        public void RefreshGrid()
        {
            dataGridView1.DataSource = GetSettingsInformation().ToArray();

            dataGridView1.Columns[NameIndex].ReadOnly = true;
            dataGridView1.Columns[NameIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            dataGridView1.Columns[TypeIndex].ReadOnly = true;
            dataGridView1.Columns[TypeIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            dataGridView1.Columns[ValueIndex].ReadOnly = false;
            dataGridView1.Columns[ValueIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        protected internal class SettingInformation
        {
            public string Name
            {
                get;
                set;
            }

            public string Type
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try 
            { 
                //var gridView = (DataGridView)sender;
                //var type = Settings.Instance.GetType();

                //string name = gridView[NameIndex, e.RowIndex].Value.ToString();
                //var value = gridView[e.ColumnIndex, e.RowIndex].Value;
                //var field = type.GetField(name);

                //field.SetValue(Settings.Instance, Convert.ChangeType(value,field.FieldType));
            }
            catch(Exception x)
            {
                MessageBox.Show("Cannot Save Setting : " + x.ToString());
                RefreshGrid();
            }
        }
        private IEnumerable<SettingInformation> GetSettingsInformation()
        {
            //var type = Settings.Instance.GetType();
            //foreach (var p in type.GetFields())
            //    yield return new SettingInformation() { Name = p.Name, Type = p.FieldType.Name, Value = (p.GetValue(Settings.Instance) ?? "").ToString() };
            return null;
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

    }
}
