using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Plex.MobileHub.Devel.Views;
using Plex.MobileHub.Devel.Logic;
using Plex.MobileHub.Devel.DevelService;
namespace Plex.MobileHub.Devel.Views
{
    public partial class AppTableColumnDetails : UserControl
    {
        const int PropertyNameIndex = 0;
        const int PropertyValueIndex = 1;

        public AppTableColumnDetails(APP_TABLE_COLUMNS Column)
        {
            InitializeComponent();

            var rows = ExtractGridInfo(Column);
            SetGridView(rows);
        }

        void SetGridView(IEnumerable<ColumnInformation> rows)
        {
            dataGridView1.DataSource = rows.ToArray();
            dataGridView1.Columns[PropertyNameIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[PropertyValueIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.ReadOnly = true;
        }

        IEnumerable<ColumnInformation> ExtractGridInfo(APP_TABLE_COLUMNS Column)
        {
            var Collection = new List<ColumnInformation>();
            Type t =  Column.GetType();
            var fields = t.GetProperties();
            foreach (var p in fields)
                Collection.Add(new ColumnInformation() {Property = p.Name, Value = (p.GetValue(Column) ?? string.Empty).ToString() });
            return Collection;
        }

        protected internal class ColumnInformation
        {
            public string Property
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

        private void button1_Click(object sender, EventArgs e)
        {
            var form = FindForm();
            form.Close();
        }
    }
}
