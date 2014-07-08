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
            host = new ServiceHost(new ClientCommandService(), new Uri ( "net.pipe://localhost" ));
            host.AddServiceEndpoint(typeof(IClientCommandService), new NetNamedPipeBinding(), "ClientCommandInterface");
        }

        public void OnDebug(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            host.BeginOpen(OnOpen, host);
        }
        
        protected override void OnStop()
        {
            consumer.Dispose();
            host.Close();
        }
        void OnOpen(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndOpen(ar);
        }
    }
}
