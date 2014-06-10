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
    public partial class CreateUser : UserControl
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            Manager mgr = Manager.Instance;
            
            string username = textBox1.Text;
            string password = textBox2.Text;
            
            using(var service = mgr.GetGeneralService())
                service.ClientUserAdd(mgr.ClientId, username, password);
        }
    }
}
