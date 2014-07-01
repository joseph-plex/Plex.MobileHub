using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    public interface IClientCallback
    {
        void IUD();
        void Query();
        void ExecuteRegisteredQuery();
        void Synchronize();
    }
}
