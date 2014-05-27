using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Manager.Views;
namespace Plex.MobileHub.Manager.Windows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            tabPage1.Controls.Add(new ClientStatus());
            tabPage2.Controls.Add(new AppInfo());
            tabPage3.Controls.Add(new LogView());
            tabPage4.Controls.Add(new ConnectionView());


        }
    }
}
