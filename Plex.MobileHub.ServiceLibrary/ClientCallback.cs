using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.Data;
namespace Plex.MobileHub.ServiceLibrary
{
    //todo implement additional constructors for Plex.MobileHub.ServiceLibrary.ClientCallback
    public class ClientCallback : IClientCallback, IRepositoryEntry
    {

        public String IpAddress {get;set;}
        public Int32 ClientId {get;set;}
        public String Token {get;set;}
        public Int32 Port {get;set;}

        ChannelFactory<IClientCallback> factory;
        IClientCallback channel;

        //To make IRepository be happy.
        public ClientCallback() { }
        //warning no check for validity of URI 
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
        
        public virtual IList<string> GetPrimaryKeys()
        {
            return new List<String> { "ClientId" };
        }

        public virtual void IUD()
        {
            channel.IUD();
        }

        public virtual QryResult Query(string connectionString, string query, params object[] arguments)
        {
            return channel.Query(connectionString, query, arguments);
        }

        public virtual RegisteredQueryResult ExecuteRegisteredQuery(string connetionString, string queryName, DateTime? time)
        {
            return channel.ExecuteRegisteredQuery(connetionString, queryName, time);
        }

        public virtual void Synchronize()
        {
            channel.Synchronize();
        }
        public virtual void Heartbeat()
        {
            channel.Heartbeat();
        }

        public void Dispose()
        {
            //todo implement IDisposable Pattern.
            throw new NotImplementedException();
        }
    }
}
