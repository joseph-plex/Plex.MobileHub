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
    public partial class AppAddForm : Form
    {
        public string Title;
        public int AppId;
        public string AuthKey;
        public string Description;
        public bool IsPrivate;

        public AppAddForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AppId = Convert.ToInt32(textBox1.Text);
                Title = textBox2.Text;
                AuthKey = textBox3.Text;
                Description = textBox4.Text;
                IsPrivate = checkBox1.Checked;
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
