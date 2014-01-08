using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plex.Pmh.Manager
{
    public partial class ClientAddForm : Form
    {



        public String ClientKey;
        public String Description;
        public bool Createable;

        public ClientAddForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Description = textBox1.Text;
            ClientKey = textBox2.Text;
            Createable = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Createable = false;
        }
    }
}
