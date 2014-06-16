using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MobileHubClient.PMH;

namespace MobileHubClient.Core
{
    interface IClientStateBehaviour
    {
        //todo Change this to client_db_companies
        IReadOnlyCollection<CLIENT_DB_COMPANIES> DbConnections
        {
            get;
        }
        
        void LogOn();
        void LogOff();

        IDbConnection GetOpenConnection(String Code);

        //todo This need to be implemented at some point.
        //void AddDatabase();
        //void DeleteDatabase();

    }
}
