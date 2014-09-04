using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using Pmh.ServiceLibrary.Types;
namespace Pmh.ServiceLibrary

{
    [ServiceContract]
    public interface IClientService 
    {
        String LogIn(Int32 ClientId, String ClientKey, String ipAddress, Int32 Port);
        //warning would be wise to make this return something
        void LogOut(String token);
    }
}
