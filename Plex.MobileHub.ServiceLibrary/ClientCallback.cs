using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibrary
{
    //todo implement additional constructors for Plex.MobileHub.ServiceLibrary.ClientCallback
    public class ClientCallback : IClientCallback
    {
        ChannelFactory<IClientCallback> factory;
        IClientCallback channel;

        //warning no check for validity of IP address
        public ClientCallback(String uri)
        {
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                UseDefaultWebProxy = false,
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647
            };

            factory = new ChannelFactory<IClientCallback>(binding, new EndpointAddress(uri));
            factory.Open();
            channel = factory.CreateChannel();
        }

        void IClientCallback.IUD()
        {
            throw new NotImplementedException();
        }

        QryResult IClientCallback.Query(string connectionString, string query, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        RegisteredQueryResult IClientCallback.ExecuteRegisteredQuery(string connetionString, string queryName, DateTime? time)
        {
            throw new NotImplementedException();
        }

        void IClientCallback.Synchronize()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
