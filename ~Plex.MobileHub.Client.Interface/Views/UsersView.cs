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
                LoadUser(mgr.ClientId, item);
      
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if (view == null) return;
            if (view.SelectedRows.Count != 1) return;
            DataGridViewRow selectedRow = view.SelectedRows[0];
            var dbCompanyId = Convert.ToInt32(selectedRow.Cells["Column6"].Value);

            var rows = dataGridView2.Rows.Cast<DataGridViewRow>();

            foreach(var row in rows)
                row.Visible = false;

            foreach (var v in rows.Where(p => Convert.ToInt32(p.Cells["Column8"].Value) == dbCompanyId))
                v.Visible = true;
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
    
        void SaveScreenData()
        {
            var mgr = Manager.Instance;
            var ClientId = mgr.ClientId;
            var UserId = Convert.ToInt32(textBox1.Text);
            //var CompanyName = listBox1.SelectedItem as String;
            //var dbRows = .Cast<DataGridViewRow>();
            var dbuaRows = dataGridView2.Rows.Cast<DataGridViewRow>().Where(p=> ((DataGridViewCheckBoxCell)p.Cells[0]).EditingCellValueChanged);
            //var AccessibleRows = dbuaRows.Where(p => Convert.ToBoolean(p.Cells["Column5"].EditedFormattedValue));
            var dbRows = dataGridView1.Rows.Cast<DataGridViewRow>().Where(p => Convert.ToBoolean(p.Cells[0].EditedFormattedValue)
                || dbuaRows.Any(d => d.Cells[4].EditedFormattedValue.Equals(p.Cells[2].EditedFormattedValue) && p.Cells[0].EditedFormattedValue.Equals(false)));
            using(var Service = mgr.GetGeneralService())
            { 
                //foreach(DataGridViewRow row in dataGridView1.Rows)
                foreach (DataGridViewRow row in dbRows)
                {
                    var dbCompanyId = Convert.ToInt32(row.Cells["Column6"].EditedFormattedValue);
                    dbCompanyId = Service.ClientDbCompanyUserAdd(dbCompanyId, UserId, null);
                    foreach (var aRow in dbuaRows)
                    {
                        if (Convert.ToBoolean(aRow.Cells[0].EditedFormattedValue))
                            Service.ClientDbCompanyUserAppsAdd(Convert.ToInt32(aRow.Cells["Column7"].EditedFormattedValue), dbCompanyId, null);
                            //else
                    }
                }
                //else
                //{
                //    Service.ClientDbCompanyUserRemove(dbCompanyId);
                    //Service.ClientDbCompanyRemove(dbCompanyId);
                //}
                //if(dataGridView1.Rows.Contains(row))
                //{
                //}
                //foreach()
                //The nullable part is to satisify the compiler; however, Column6 should always have a value.

                //var dbCompanyId = Convert.ToInt32(row.Cells["Column6"].Value);
                //If the user specifies to create the relationship or any child permissions require its creation set the create flag to true.
                //bool ShouldExist = (Convert.ToBoolean(row.Cells["Column2"].EditedFormattedValue)) ? true : dbuaRows.Any(p => Convert.ToBoolean(p.Cells["Column5"].EditedFormattedValue));

                //if (ShouldExist)
                    //dbCompanyId = Service.ClientDbCompanyUserAdd(dbCompanyId, UserId, null);

                //foreach (var permissionForCreation in dbuaRows.Where(p => Convert.ToBoolean(p.Cells["Column5"].EditedFormattedValue)))
                    //Service.ClientDbCompanyUserAppsAdd(Convert.ToInt32(permissionForCreation.Cells["Column7"].EditedFormattedValue), dbCompanyId, null);
            }
        }

        void LoadUser(int clientId, string username)
        {
            Manager mgr = Manager.Instance;
            String sqlUserInformation = "select * from client_users where client_id = :a and name = :b and rownum >= 1";

            var general = mgr.Query(sqlUserInformation, clientId, username);
            int UserId = Convert.ToInt32(general.GetValue("user_id", 0));

            textBox1.Text = UserId.ToString();
            textBox2.Text = general.GetValue("name", 0).ToString();
            textBox3.Text = general.GetValue("password", 0).ToString();

            var companies = mgr.Query(DbResource.UserClientCompanyPermission, UserId);
            var applications = mgr.Query(DbResource.UserCompanyDbAppPermission, UserId);


            //Do applications (gridview2) first so that implicit selection in gridview1 will still show.
            //Increase performance by getting indexes ahead of time.
            var AppIdIndex = applications.IndexOf("App_id");
            var appNameIndex = applications.IndexOf("title");
            var AppDbId = applications.IndexOf("db_company_id");
            var appDescIndex = applications.IndexOf("description");
            var appHasAccessIndex = applications.IndexOf("has_access");

            dataGridView2.Rows.Clear();
            for (int i = 0; i < applications.Rows.Count; i++)
            {
                dataGridView2.Rows.Add(applications.GetValue(appHasAccessIndex, i), applications.GetValue(appNameIndex, i), applications.GetValue(appDescIndex, i), applications.GetValue(AppIdIndex, i),applications.GetValue(AppDbId,i));
                dataGridView2.Rows[i].Visible = false;
            }

            //Increase performance by getting indexes ahead of time.
            var coHasAccessIndex = companies.IndexOf("has_access");
            var companyCodeIndex = companies.IndexOf("companycode");
            var dbCompanyIndex = companies.IndexOf("db_company_id");

            dataGridView1.Rows.Clear();
            for(int i = 0 ; i < companies.Rows.Count; i++)
                dataGridView1.Rows.Add(companies.GetValue(coHasAccessIndex,i), companies.GetValue(companyCodeIndex,i), companies.GetValue(dbCompanyIndex,i));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Manager mgr = Manager.Instance;
            var item = listBox1.SelectedItem as String;
            if (item != null)
                LoadUser(mgr.ClientId, item);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveScreenData();
            toolStripButton1_Click(this, EventArgs.Empty);
        }
    }
}
