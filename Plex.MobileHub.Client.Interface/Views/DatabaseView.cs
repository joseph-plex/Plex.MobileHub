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
            Action action = () => win32.ShowDialog(this);
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
                    foreach (var Info in DbInfo.CompanyConnectionPairings)
                    {
                        NaviBand band = new NaviBand(components);

                        naviBar1.ActiveBand = band;
                        band.ClientArea.Location = new System.Drawing.Point(0, 0);
                        band.ClientArea.Name = "ClientArea";
                        band.ClientArea.Size = new System.Drawing.Size(208, 300);
                        band.ClientArea.TabIndex = 0;
                        band.Location = new System.Drawing.Point(1, 27);
                        band.Name = "naviBand1";
                        band.Text = Info.Key;
                        band.Size = new System.Drawing.Size(208, 300);
                        band.TabIndex = 3;

                        naviBar1.Controls.Add(band);
                    }
                    splitContainer2.Panel1Collapsed = false;
                    break;
            }
        }
    }
}
