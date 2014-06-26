using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Channels;

using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
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

        public MethodResult ConnectionConnect(int clientid, int appid, string database, string user, string password) 
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.ConnectionConnect(clientid, appid, database, user, password);
        }
        public MethodResult ConnectionRelease(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.ConnectionRelease(connectionId);
        }
        public MethodResult ConnectionStatus(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.ConnectionStatus(connectionId);
        }
        public MethodResult DeviceRequestId(int connectionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.DeviceRequestId(connectionId);
        }
        public MethodResult IUD(int connection, object IUDData)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.IUD(connection, IUDData);
        }
        public void QryExecute(int connectionId, string QueryName)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.QryExecute(connectionId, QueryName);
        }
        public PlexQueryResult QueryDatabase(int connectionId, string Query)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.QueryDatabase(connectionId, Query);
        }
        public void DeviceSynchronize(int connectionId, int versionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            channel.DeviceSynchronize(connectionId, versionId);
        }
        public void Dispose()
        {
            serviceFactory.Close();
        }
    }
}
