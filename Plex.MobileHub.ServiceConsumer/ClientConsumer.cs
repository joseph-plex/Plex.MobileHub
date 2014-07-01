using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
namespace Plex.MobileHub.ServiceConsumer
{
    public class ClientConsumer : IClientService, IDisposable
    {
        DuplexChannelFactory<IClientService> serviceFactory { get; set; }

        public ClientConsumer(String endpointUri, IClientCallback ClientCallback)
        {
            WSDualHttpBinding binding = new WSDualHttpBinding();
            EndpointAddress address = new EndpointAddress(endpointUri);
            InstanceContext context = new InstanceContext(ClientCallback);
            serviceFactory = new DuplexChannelFactory<IClientService>(context, binding, address);
        }

        public void LogIn(int clientId, String clientKey)
        {
            IClientService service = serviceFactory.CreateChannel();
            service.LogIn(clientId, clientKey);
        }

        public void EnableConnections(String IPAddress, Int32 Port)
        {
            IClientService service = serviceFactory.CreateChannel();
            service.EnableConnections(IPAddress, Port);
        }

        public void DisableConnections()
        {
            IClientService service = serviceFactory.CreateChannel();
            service.DisableConnections();
        }

        public void LogOut()
        {
            IClientService service = serviceFactory.CreateChannel();
            service.LogOut();
        }

        public void Dispose()
        {
            serviceFactory.Close();
        }
    }
}
