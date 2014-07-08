using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
namespace Plex.MobileHub.ServiceConsumer
{
    public class ClientConsumer : IClientService, IDisposable
    {
        public IClientCallback ClientCallback
        {
            get;
            private set;
        }

        DuplexChannelFactory<IClientService> serviceFactory { get; set; }

        public ClientConsumer(String endpointUri, IClientCallback clientCallback)
        {
            ClientCallback = clientCallback;
            WSDualHttpBinding binding = new WSDualHttpBinding();
            EndpointAddress address = new EndpointAddress(endpointUri);
            InstanceContext context = new InstanceContext(ClientCallback);
            serviceFactory = new DuplexChannelFactory<IClientService>(context, binding, address);
        }

        public void LogIn(int clientId, String clientKey)
        {
            IClientService service = serviceFactory.CreateChannel();
            service.LogIn(clientId, clientKey);
        }


        public void LogOut()
        {
            IClientService service = serviceFactory.CreateChannel();
            service.LogOut();
        }

        public void Dispose()
        {
            serviceFactory.Close();
        }

        public IEnumerable<Data.Types.APPS> GetAllAPPS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.APPS SelectAPPS(Predicate<Data.Types.APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.APP_QUERIES> GetAllAPP_QUERIES()
        {
            throw new NotImplementedException();
        }

        public Data.Types.APP_QUERIES SelectAPP_QUERIES(Predicate<Data.Types.APP_QUERIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<Data.Types.APP_QUERY_COLUMNS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.CLIENT_APPS> GetAllCLIENT_APPS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.CLIENT_APPS SelectCLIENT_APPS(Predicate<Data.Types.CLIENT_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public Data.Types.CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<Data.Types.CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANIES(Data.Types.CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANIES(Data.Types.CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANIES(Predicate<Data.Types.CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<Data.Types.CLIENT_DB_COMPANY_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USERS(Data.Types.CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USERS(Data.Types.CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USERS(Predicate<Data.Types.CLIENT_DB_COMPANY_USERS> value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<Data.Types.CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS(Data.Types.CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS(Data.Types.CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<Data.Types.CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.CLIENT_USERS> GetAllCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.CLIENT_USERS SelectCLIENT_USERS(Predicate<Data.Types.CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_USERS(Data.Types.CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_USERS(Data.Types.CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_USERS(Predicate<Data.Types.CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Types.LOGS> GetAllLOGS()
        {
            throw new NotImplementedException();
        }

        public Data.Types.LOGS SelectLOGS(Predicate<Data.Types.LOGS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertLOGS(Data.Types.LOGS value)
        {
            throw new NotImplementedException();
        }
    }
}
