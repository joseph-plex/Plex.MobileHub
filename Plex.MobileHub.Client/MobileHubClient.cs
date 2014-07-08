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
        ServiceHost host;
        public MobileHubClient()
        {
            InitializeComponent();
            consumer = new ClientConsumer(String.Empty, new ClientCallback());
            //Should be concrete class
            host = new ServiceHost(typeof(IClientCommandService), new Uri ( "net.pipe://localhost" ));
            host.AddServiceEndpoint(typeof(IClientCommandService), new NetNamedPipeBinding(), "ClientCommandInterface");
        }

        public void OnDebug(string[] args)
        {
            OnStart(args);
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
