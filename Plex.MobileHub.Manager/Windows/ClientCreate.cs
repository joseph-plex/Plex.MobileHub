using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Manager.Data;

namespace Plex.MobileHub.Manager.Windows
{
    public partial class ClientCreate : Form
    {
        public ClientCreate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             try { 
                DataFactory factory = new DataFactory();
       
                int clientId = Convert.ToInt32(textBox1.Text);
                string clientKey = Convert.ToString(textBox2.Text);
                string description = Convert.ToString(textBox3.Text);

                factory.NonQuery("INSERT INTO CLIENTS(CLIENT_ID, CLIENT_KEY, DESCRIPTION) VALUES (:A,:B,:C)", clientId, clientKey, description);
            }
            catch(Exception x) {
                MessageBox.Show(x.Message);
            }
        }
    }
}
