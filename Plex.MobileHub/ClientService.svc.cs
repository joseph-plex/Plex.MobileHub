using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries;

namespace Plex.MobileHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Client" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Client.svc or Client.svc.cs at the Solution Explorer and start debugging.
    public class ClientService :  IClientService
    {
        public IClientCallback ClientCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IClientCallback>();
            }
        }
        public void LogIn(Int32 clientId, String clientKey)
        {
            LogIn logIn = new LogIn();
            logIn.ClientsRepository = new OracleRepository<CLIENTS>();
            logIn.Strategy(clientId, clientKey);
        }

        public void LogOut(){
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
