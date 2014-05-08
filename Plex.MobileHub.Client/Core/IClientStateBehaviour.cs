using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHubClient.Core
{
    interface IClientStateBehaviour
    {
        ClientService Context
        {
            get;
            set;
        }

        void LogOn();
        void LogOff();
    }
}
