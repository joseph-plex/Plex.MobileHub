using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guifreaks.NavigationBar;
using Plex.MobileHub.Client.Interface.Windows;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            SetClientUsers();
        }
        private void naviBar1_Resize(object sender, EventArgs e)
        {
            var obj = ((NaviBar)sender);
            if (obj.Collapsed)
            {
                splitContainer1.SplitterDistance = obj.Size.Width;
                splitContainer1.IsSplitterFixed = true;
                splitContainer1.FixedPanel = FixedPanel.Panel1;
            }
            else
            {
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.SplitterDistance = obj.Size.Width;
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            naviBar1.Width = splitContainer1.SplitterDistance;
        }

        void SetClientUsers()
        {
            foreach(var row in Manager.Instance.Query(DbResource.UserClientCompany, Manager.Instance.ClientId).Rows)
                listBox1.Items.Add(Convert.ToString(row.Values[0]));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lbox = (ListBox)sender;
            Manager mgr = Manager.Instance;
            var item = lbox.SelectedItem as String;
            if (item != null) 
            { 
                string sql = "select * from client_users where client_id = :a and name = :b and rownum >= 1";
                var general = mgr.Query(sql, mgr.ClientId, item);

                var name = Convert.ToString(general.GetValue("name",0));
                var userId = Convert.ToInt32(general.GetValue("user_id",0));
                var password = Convert.ToString(general.GetValue("password",0));
                var clientId = Convert.ToInt32(general.GetValue("client_id",0));

                textBox1.Text = userId.ToString();
                textBox2.Text = name.ToString();
                textBox3.Text = password.ToString();

                var c = mgr.Query(DbResource.UserClientCompanyPermission, userId);

                dataGridView1.Rows.Clear();

                var CII = c.GetColumnIndex("CompanyId");//Company Id Index
                var HAI = c.GetColumnIndex("has_access");//Has Access Index
                var CCI = c.GetColumnIndex("companycode");//Company Code Index 

                for (int i = 0 ; i < c.Rows.Count; i++)
                    dataGridView1.Rows.Add(c.GetValue(HAI, i), c.GetValue(CCI, i), c.GetValue(CII, i));
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (dataGridView1.SelectedRows.Count != 1)
                return;
            Manager mgr = Manager.Instance;
            var row = dataGridView1.SelectedRows[0];
            var companyCode = Convert.ToInt32(row.Cells["Column6"].Value);
            var result = mgr.Query(DbResource.UserCompanyDbAppPermission, companyCode, Convert.ToInt32(textBox1.Text));

            for (int i = 0; i < result.Rows.Count; i++)
                dataGridView2.Rows.Add(
                    Convert.ToBoolean(result.Rows[i].Values[0]),
                    Convert.ToString(result.Rows[i].Values[1]),
                    Convert.ToString(result.Rows[i].Values[2])
                );
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            WinFactory factory = new WinFactory();
            switch(factory.CreateUserCreateWindow().ShowDialog())
            {
                case DialogResult.OK:
                    listBox1.Items.Clear();
                    SetClientUsers();
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //todo implement adding db_company permissions
        }
    }
}
