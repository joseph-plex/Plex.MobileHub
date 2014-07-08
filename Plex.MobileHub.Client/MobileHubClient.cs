using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.ServiceConsumer;

namespace Plex.MobileHub.Client
{
    public partial class MobileHubClient : ServiceBase
    {
        ClientConsumer consumer;
        public MobileHubClient()
        {
            InitializeComponent();
            consumer = new ClientConsumer(String.Empty, new ClientCallback());
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
            consumer.Dispose();
        }

    }
}
