using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plex.MobileHub.Devel.Windows
{
    public partial class ActionForm : Form
    {
        public ActionForm()
        {
            InitializeComponent();
        }

        public ActionForm(UserControl control): this()
        {
            Controls.Add(control);
        }
    }
}
