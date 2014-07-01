using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        String LogIn(Int32 ClientId, String ClientKey);

        [OperationContract(IsOneWay = true)]
        void LogOut(String Key);
    }
}
