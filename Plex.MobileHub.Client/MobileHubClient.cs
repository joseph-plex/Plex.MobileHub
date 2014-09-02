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

namespace Plex.MobileHub.Client
{
    public partial class MobileHubClient : ServiceBase
    {
        //public ClientConsumer Consumer { get; private set; }
        ServiceHost host;

        public MobileHubClient()
        {
            //InitializeComponent();
            //Consumer = new ClientConsumer(String.Empty, new ClientCallback());
            //host = new ServiceHost(new ClientCommandService(this), new Uri ( "net.pipe://localhost" ));
            //host.AddServiceEndpoint(typeof(IClientCommandService), new NetNamedPipeBinding(), "ClientCommandInterface");
        }

        public void OnDebug(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            host.BeginOpen(OnOpen, host);
        }
        
        //protected override void OnStop()
        //{
        //    Consumer.Dispose();
        //    host.Close();
        //}
        void OnOpen(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndOpen(ar);
        }
    }
}
