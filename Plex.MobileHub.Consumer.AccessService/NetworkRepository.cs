using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.ServiceModel;
using Plex.MobileHub.ServiceLibrary.AccessService;
namespace Plex.MobileHub.Consumer
{
    public class NetworkRepository<T> : IRepository<T> where T : IRepositoryEntry, new()
    {
        public event EventHandler<RepositoryOperationEventArgs> InsertEvent;

        public event EventHandler<RepositoryOperationEventArgs> UpdateEvent;

        public event EventHandler<RepositoryOperationEventArgs> DeleteEvent;

        protected ChannelFactory<IAccessService> Factory
        {
            get
            {
                return factory;
            }
        }

        private ChannelFactory<IAccessService> factory;
        #region Public Methods
        public NetworkRepository()
        {
            //todo Put some stuff in here for default ctor
        }

        public NetworkRepository(string uri)
        {
            EndpointAddress address = new EndpointAddress(uri);
            BasicHttpBinding binding = new BasicHttpBinding() { UseDefaultWebProxy = false };
            factory = new ChannelFactory<IAccessService>(binding, address);
            factory.Open();
        }

        public NetworkRepository(ChannelFactory<IAccessService> channelFactory)
        {
            factory = channelFactory;
        }
        #endregion
        #region IRepository Implementation
        public IList<string> PrimaryKeys
        {
        }

        public void Insert(T Entry)
        {
            throw new NotImplementedException();
        }

        public void Update(T Entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public T Retrieve(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> RetrieveAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
