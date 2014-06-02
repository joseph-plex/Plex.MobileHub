using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace MobileHubClient.Channels
{
    class DefaultClosedStateBehavior : IClientChannelStateBehavior<IClientChannel>
    {
        Type ServiceType;
        public IClientChannel Parent
        {
            get;
            set;
        }

        public bool Open()
        {
            List<Uri> Uris = new List<Uri>();
            Uris.Add(new Uri(@"net.pipe://localhost/" + @Parent.Name));
            Uris.Add(new Uri("http://" + Parent.IP + ":" + Parent.Port + "/" + Parent.Name));

            Parent.Service = new ServiceHost(ServiceType, Uris.ToArray());

            var webs = new WSHttpBinding();
            var pipe = new NetNamedPipeBinding();
            webs.Security.Mode = SecurityMode.None;
            pipe.Security.Mode = NetNamedPipeSecurityMode.None;

            //decribe how metadata behaves
            Parent.Service.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpsGetEnabled = true });

            //add bindings to site
            Parent.Service.AddServiceEndpoint(ServiceType, webs, "webs");
            Parent.Service.AddServiceEndpoint(ServiceType, pipe, "pipe");

            Parent.Service.BeginOpen((IAsyncResult ar) => { ((ServiceHost)ar.AsyncState).EndOpen(ar); }, Parent.Service);
            Parent.CurrentState = IClientChannelState.Opened;
            return true;
        }

        public bool Close()
        {
            throw new InvalidOperationException("The Channel is already Closed");
        }

        private DefaultClosedStateBehavior(Type T)
        {
            ServiceType = T;
        }

        public static DefaultClosedStateBehavior Create(IClientChannel Channel, Type T)
        {
            return new DefaultClosedStateBehavior(T) { Parent = Channel };
        }
    }
}
