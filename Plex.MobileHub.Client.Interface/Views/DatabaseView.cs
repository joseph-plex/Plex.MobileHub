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
        public DatabaseView()
        {
            InitializeComponent();
            Init();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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
                    foreach (var companyCode in DbInfo.CompanyConnectionPairings)
                        naviBar1.Bands.Add(new NaviBand() { Text = companyCode.Key });
                    MessageBox.Show(naviBar1.Bands.Count.ToString());
                    splitContainer2.Panel1Collapsed = false;
                    break;
            }
        }
    }
}
