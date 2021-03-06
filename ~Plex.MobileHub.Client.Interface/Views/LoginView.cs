﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        void LoginView_Load(object sender, EventArgs e)
        {
            Manager mgr = Manager.Instance;
            textBox3.Text = mgr.IPAddress;
            textBox4.Text = mgr.Port.ToString();
            checkBox1.Checked = mgr.AutoLogin;
        }

        void AcceptButton_Click(object sender, EventArgs e)
        {
            try {
                Manager mgr = Manager.Instance;

                var port = Int32.Parse(textBox4.Text);
                var address = IPAddress.Parse(textBox3.Text);

                mgr.IPAddress = address.ToString();
                mgr.AutoLogin = checkBox1.Checked;
                mgr.Port = port;
            }
            catch (Exception x){
                MessageBox.Show(x.ToString());
            }
        }
    }
}
