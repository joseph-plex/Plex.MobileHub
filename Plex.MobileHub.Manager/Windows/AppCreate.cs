using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Manager.Data;
namespace Plex.MobileHub.Manager.Windows
{
    public partial class AppCreate : Form
    {
        public const string insertApp = "insert into APPS(APP_ID, AUTH_KEY, TITLE, DESCRIPTION, IS_CLIENT_CUSTOM_APP) values (:a, :b, :c, :d, :e)";

        public AppCreate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataFactory factory = new DataFactory();
            var app_id = Convert.ToInt32(factory.Query("select ID_GEN.nextval from dual")[0,0]);
            factory.NonQuery(insertApp, app_id, textBox1.Text, textBox2.Text,  textBox4.Text, Convert.ToInt32(checkBox1.Checked));
        }
    }
}
