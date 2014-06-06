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
            var result = Manager.Instance.Query(DbResource.UserClientCompany, Manager.Instance.ClientId);
            foreach(var row in result.Rows)
                listBox1.Items.Add(Convert.ToString(row.Values[0]));
        }

    }
}
