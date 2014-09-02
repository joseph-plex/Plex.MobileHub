using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibrary

{
    [ServiceContract]
    public interface IClientCallback : IDisposable
    {
        [OperationContract]
        void IUD();

        [OperationContract]
        QryResult Query(String connectionString, String query, params object [] arguments);

        [OperationContract]
        RegisteredQueryResult ExecuteRegisteredQuery(String connetionString, String queryName, DateTime? time = null);

        [OperationContract]
        void Synchronize();

        [OperationContract]
        void Heartbeat();
    }
}
