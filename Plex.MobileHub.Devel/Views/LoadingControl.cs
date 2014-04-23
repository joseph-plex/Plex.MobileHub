using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Plex.MobileHub.Devel.Views
{
    public partial class LoadingControl : UserControl
    {
        Timer timer;

        public LoadingControl()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
                progressBar1.Value = progressBar1.Minimum;
            progressBar1.PerformStep();
        }
    }
}
