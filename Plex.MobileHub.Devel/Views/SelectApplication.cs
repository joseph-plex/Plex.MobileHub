using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plex.MobileHub.Devel.Logic;
using Plex.MobileHub.Devel.Windows;
namespace Plex.MobileHub.Devel.Views
{
    public partial class SelectApplication : UserControl
    {
        public SelectApplication()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormFactory factory = new FormFactory();
            var form =  factory.CreateLoadingForm(() => ProcessData());
            form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        void ProcessData()
        {
            if (textBox2.Text == string.Empty || textBox1.Text == string.Empty)
                throw new Exception("All Fields must be filled");

            int id;
            if (!int.TryParse(textBox2.Text, out id))
                throw new Exception("Invalid Application Id Specified");

            string key = textBox1.Text;

            DevelManager.Instance.ChangeProject(id, key);

            var Form = FindForm();
            Form.InvokeIfRequired(() => Form.Close());
        }

    }
}
