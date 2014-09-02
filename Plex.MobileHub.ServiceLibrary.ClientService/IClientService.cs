using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.Types;
using Plex.MobileHub.ServiceLibrary;
namespace Plex.MobileHub.ServiceLibrary.ClientService

{
    [ServiceContract]
    public interface IClientService 
    {
        String LogIn(Int32 ClientId, String ClientKey, String ipAddress, Int32 Port);
        void LogOut(String token);
    }
}
