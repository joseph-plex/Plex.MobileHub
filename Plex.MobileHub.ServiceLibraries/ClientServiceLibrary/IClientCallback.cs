using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    public interface IClientCallback : IDisposable
    {
        [OperationContract]
        void IUD();

        [OperationContract]
        QryResult Query(String connectionString, String query, params object [] arguments);

        [OperationContract]
        RegisteredQueryResult ExecuteRegisteredQuery(String connetionString, String queryName, DateTime? time = null);

        [OperationContract(IsOneWay=true)]
        void Synchronize();
    }
}
