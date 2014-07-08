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
        public IRepository<CLIENTS> CLIENTS
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<ClientInformation> ClientInfo
        {
            get
            {
                return clientInfo;
            }
            set
            {
                clientInfo = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }

        event EventHandler RepositoryChangedEvent;

        IRepository<CLIENTS> clients;
        IRepository<ClientInformation> clientInfo;

        LogIn logIn;

        public ClientService()
        {
            CLIENTS = new OracleRepository<CLIENTS>();
            ClientInfo = Singleton<InMemoryRepository<ClientInformation>>.Instance;
            
            logIn = new LogIn();
            RepositoryChangedEvent += (s, e) => init();
            init();
        }

        void init()
        {
            logIn.ClientsRepository = CLIENTS;
            logIn.ClientInfoRepository = ClientInfo;
        }
        public IClientCallback ClientCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IClientCallback>();
            }
        }

        public void LogIn(Int32 clientId, String clientKey)
        {
            logIn.Strategy(clientId, clientKey, ClientCallback);
        }

        public void LogOut(){
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }




        public void GetAllAPPS()
        {
            throw new NotImplementedException();
        }

        public void SelectAPPS(Predicate<APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public void GetAllAPP_QUERIES()
        {
            throw new NotImplementedException();
        }

        public void SelectAPP_QUERIES(Predicate<APP_QUERIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS()
        {
            throw new NotImplementedException();
        }

        public APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<APP_QUERY_COLUMNS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CLIENT_APPS> GetAllCLIENT_APPS()
        {
            throw new NotImplementedException();
        }

        public CLIENT_APPS SelectCLIENT_APPS(Predicate<CLIENT_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CLIENT_USERS> GetAllCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public CLIENT_USERS SelectCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_USERS(CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_USERS(CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LOGS> GetAllLOGS()
        {
            throw new NotImplementedException();
        }

        public LOGS SelectLOGS(Predicate<LOGS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertLOGS(LOGS value)
        {
            throw new NotImplementedException();
        }
    }
}
