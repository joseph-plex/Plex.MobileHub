using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                Manager mgr = Manager.Instance;

                var clientKey = textBox1.Text;
                var clientId = Int32.Parse(textBox2.Text);

                mgr.ClientKey = clientKey;
                mgr.ClientId = clientId;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void AccountView_Load(object sender, EventArgs e)
        {
            Manager mgr = Manager.Instance;
            textBox1.Text = mgr.ClientKey;
            textBox2.Text = mgr.ClientId.ToString();
        }
    }
}
