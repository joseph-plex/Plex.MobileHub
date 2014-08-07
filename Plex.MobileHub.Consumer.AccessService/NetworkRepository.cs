using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
        public IList<string> GetPrimaryKeys()
        {
            return factory.CreateChannel().GetPrimayKeys(typeof(T).FullName);
        }
        public IList<string> PrimaryKeys()
        {
            return factory.CreateChannel().GetPrimayKeys(typeof(T).FullName);
        }

        public void Insert(T Entry)
        {
            factory.CreateChannel().Insert(typeof(T).FullName, Entry);
        }

        public void Update(T Entry)
        {
            factory.CreateChannel().Update(typeof(T).FullName, Entry);
        }

        public void Delete(Predicate<T> predicate)
        {
            var entries = RetrieveAll().Where(new Func<T,bool>(predicate)).Cast<Object>();
            factory.CreateChannel().Delete(typeof(T).FullName, entries.ToArray());
        }

        public bool Exists(Predicate<T> predicate)
        {
            return RetrieveAll().Any(new Func<T, Boolean>(predicate));
        }

        public T Retrieve(Predicate<T> predicate)
        {
            return RetrieveAll().FirstOrDefault(new Func<T, Boolean>(predicate));
        }

        public int Count()
        {
            return RetrieveAll().Count();
        }

        public int Count(Predicate<T> predicate)
        {
            return RetrieveAll().Count(new Func<T,Boolean>(predicate));
        }

        public IEnumerable<T> RetrieveAll()
        {
            return factory.CreateChannel().RetrieveAll(typeof(T).FullName).Cast<T>();
        }
        #endregion

        
    }
}
