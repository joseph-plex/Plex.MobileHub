using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plex.MobileHub.Client.Interface.Logs;

namespace Plex.MobileHub.Client.Interface.Views
{
    public partial class LogsView : UserControl
    {
        public LogsView()
        {
            InitializeComponent();
            SetLogs();
        }

        void SetLogs()
        {
            using (var Service = GetLogService())
                dataGridView1.DataSource = Service.GetLogs();
        }

        LogsServiceClient GetLogService()
        {
            return new LogsServiceClient("WSDualHttpBinding_LogsService");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetLogs();
        }
    }
}
