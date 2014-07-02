using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract]
    public interface IClientService : IDisposable
    {
        IClientCallback ClientCallback { get; }


        [OperationContract(IsOneWay = true)]
        void LogIn(Int32 ClientId, String ClientKey);

        [OperationContract(IsOneWay = true)]
        void EnableConnections(String IPAddress, Int32 Port);

        [OperationContract(IsOneWay = true)]
        void DisableConnections();

        [OperationContract(IsOneWay = true)]
        void LogOut();

    }
}
