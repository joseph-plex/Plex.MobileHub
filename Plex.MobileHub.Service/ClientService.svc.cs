using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.ClientService;
namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
        public void DoWork()
        {
        }

        public ServiceLibrary.IClientCallback ClientCallback
        {
            get { throw new NotImplementedException(); }
        }

        public void LogIn(int ClientId, string ClientKey)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.APPS> GetAllAPPS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.APPS SelectAPPS(Predicate<ServiceLibrary.Types.APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.APP_QUERIES> GetAllAPP_QUERIES()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.APP_QUERIES SelectAPP_QUERIES(Predicate<ServiceLibrary.Types.APP_QUERIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<ServiceLibrary.Types.APP_QUERY_COLUMNS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.CLIENT_APPS> GetAllCLIENT_APPS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.CLIENT_APPS SelectCLIENT_APPS(Predicate<ServiceLibrary.Types.CLIENT_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANIES(ServiceLibrary.Types.CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANIES(ServiceLibrary.Types.CLIENT_DB_COMPANIES value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANIES(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANIES> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USERS(ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USERS(ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USERS(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANY_USERS> value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS(ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS(ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<ServiceLibrary.Types.CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.CLIENT_USERS> GetAllCLIENT_USERS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.CLIENT_USERS SelectCLIENT_USERS(Predicate<ServiceLibrary.Types.CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertCLIENT_USERS(ServiceLibrary.Types.CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void UpdateCLIENT_USERS(ServiceLibrary.Types.CLIENT_USERS value)
        {
            throw new NotImplementedException();
        }

        public void DeleteCLIENT_USERS(Predicate<ServiceLibrary.Types.CLIENT_USERS> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceLibrary.Types.LOGS> GetAllLOGS()
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.Types.LOGS SelectLOGS(Predicate<ServiceLibrary.Types.LOGS> predicate)
        {
            throw new NotImplementedException();
        }

        public void InsertLOGS(ServiceLibrary.Types.LOGS value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
