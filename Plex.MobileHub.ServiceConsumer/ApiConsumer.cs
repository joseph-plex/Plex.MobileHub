using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Channels;


using System.Xml.Serialization;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
namespace Plex.MobileHub.ServiceConsumer
{
    public class ApiConsumer : IApiService, IDisposable
    {
        ChannelFactory<IApiService> serviceFactory;

        public ApiConsumer(String endpointUri)
        {
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress(endpointUri);
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
        public RegisteredQueryResult QryExecute(int connectionId, string QueryName, DateTime? time = null)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.QryExecute(connectionId, QueryName, time);
        }
        public QryResult QueryDatabase(int connectionId, string Query, params object[] arguments)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.QueryDatabase(connectionId, Query, arguments);
        }
        public DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId)
        {
            IApiService channel = serviceFactory.CreateChannel();
            return channel.DeviceSynchronize(connectionId, versionId);
        }
        public void Dispose()
        {
            serviceFactory.Close();
        }
    }
}
