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


        public void SelectAPPS()
        {
            throw new NotImplementedException();
        }

        public void GetAllAPPS()
        {
            throw new NotImplementedException();
        }

        public void GetAllAPP_QUERIES()
        {
            throw new NotImplementedException();
        }

        public void SelectAPP_QUERIES()
        {
            throw new NotImplementedException();
        }

        public void GetAllAPP_QUERY_COLUMNS()
        {
            throw new NotImplementedException();
        }

        public void SelectAPP_QUERY_COLUMNS()
        {
            throw new NotImplementedException();
        }

        public void GetAllCLIENT_APPS()
        {
            throw new NotImplementedException();
        }

        public void SelectCLIENT_APPS()
        {
            throw new NotImplementedException();
        }

        public void GetAllCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public void SelectCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public void GetAllCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public void SelectCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public void GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public void SelectCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public void GetAllCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public void SelectCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public void GetAllLOGS()
        {
            throw new NotImplementedException();
        }

        public void SelectLOGS()
        {
            throw new NotImplementedException();
        }

        public void InsertLOGS()
        {
            throw new NotImplementedException();
        }
    }
}
