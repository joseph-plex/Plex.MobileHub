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

        public void ConnectionConnect(int clientid, int appid, string database, string user, string password) 
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionConnect(clientid, appid, database, user, password);
        }
        public void ConnectionRelease(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionRelease(connectionId);
        }
        public void ConnectionStatus(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.ConnectionStatus(connectionId);
        }
        public void DeviceRequestId(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.DeviceRequestId(connectionId);
        }
        public void IUD(int connection, object IUDData)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.IUD(connection, IUDData);
        }
        public void QryExecute(int connectionId, string QueryName)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.QryExecute(connectionId, QueryName);
        }
        public void QueryDatabase(int connectionId, string Query)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.QueryDatabase(connectionId, Query);
        }
        public void DeviceSynchronize(int connectionId, int versionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.DeviceSynchronize(connectionId, versionId);
        }
        public void Dispose()
        {
            ((IDisposable)serviceFactory).Dispose();
        }
    }
}
