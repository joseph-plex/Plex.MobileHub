using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHubClient.Core
{
    interface IClientStateBehaviour
    {
        //todo Change this to client_db_companies
        IReadOnlyCollection<object> DbConnections
        {
            get;
        }
        
        void LogOn();
        void LogOff();

        //todo This need to be implemented at some point.
        //void AddDatabase();
        //void DeleteDatabase();

    }
}
