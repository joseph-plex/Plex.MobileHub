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
            Parent.Service = new ServiceHost(ServiceType, new Uri("http://" + Parent.IP + ":" + Parent.Port + "/" + Parent.Name));

            var webs = new WSHttpBinding();
            var dual = new WSDualHttpBinding();
            var basic = new BasicHttpBinding();

            webs.Security.Mode = SecurityMode.None;
            dual.Security.Mode = WSDualHttpSecurityMode.None;
            basic.Security.Mode = BasicHttpSecurityMode.None;

            //decribe how metadata behaves
            Parent.Service.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpsGetEnabled = true });

            //add bindings to site
            Parent.Service.AddServiceEndpoint(ServiceType, webs, "webs");
            Parent.Service.AddServiceEndpoint(ServiceType, dual, "dual");
            Parent.Service.AddServiceEndpoint(ServiceType, basic,"basic");

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
