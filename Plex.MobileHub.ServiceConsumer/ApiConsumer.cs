using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Channels;

using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
namespace Plex.MobileHub.ServiceConsumer
{
    public class ApiConsumer : IApiService, IDisposable
    {
        ChannelFactory<IApiService> serviceFactory;

        public ApiConsumer(String endPointUri)
        {
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress(endPointUri);
            serviceFactory = new ChannelFactory<IApiService>(httpBinding, address);
        }

        public void ConnectionConnect() 
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionConnect();
        }
        public void ConnectionRelease() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionRelease();
        }
        public void ConnectionStatus() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionStatus();
        }
        public void DeviceRequestId() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.DeviceRequestId();
        }
        public void IUD() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.IUD();
        }
        public void QryExecute() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.QryExecute();
        }
        public void QueryDatabase() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.QueryDatabase();
        }
        public void DeviceSynchronization() {
            IApiService channel = serviceFactory.CreateChannel();
            channel.DeviceSynchronization();
        }
        public void Dispose()
        {
            ((IDisposable)serviceFactory).Dispose();
        }
    }
}
