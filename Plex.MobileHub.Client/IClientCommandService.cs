using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.Client
{
    [ServiceContract]
    interface IClientCommandService
    {
        [OperationContract]
        void LogIn(int id, string key);

        [OperationContract]
        void LogOff();

    }
}