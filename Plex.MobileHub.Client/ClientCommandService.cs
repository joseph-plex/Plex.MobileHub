using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.Client
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ClientCommandService : IClientCommandService
    {
        public bool LoggedIn;
        public MobileHubClient Context { get; private set; }

        public ClientCommandService(MobileHubClient context)
        {
            Context = context;
            LoggedIn = false;
        }

        public void LogIn(int id, string key)
        {
            Context.Consumer.LogIn(id, key);
            LoggedIn = true;
        }

        public void LogOff()
        {
            Context.Consumer.LogOut();
            LoggedIn = false;
        }

        public IEnumerable<APPS> GetAllAPPS()
        {
            return Context.Consumer.GetAllAPPS();
        }

        public APPS SelectAPPS(Predicate<APPS> predicate)
        {
            return Context.Consumer.SelectAPPS(predicate);
        }

        public IEnumerable<APP_QUERIES> GetAllAPP_QUERIES()
        {
            return Context.Consumer.GetAllAPP_QUERIES();
        }

        public APP_QUERIES SelectAPP_QUERIES(Predicate<APP_QUERIES> predicate)
        {
            return Context.Consumer.SelectAPP_QUERIES(predicate);
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS()
        {
            return Context.Consumer.GetAllAPP_QUERY_COLUMNS();
        }

        public APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<APP_QUERY_COLUMNS> predicate)
        {
            return Context.Consumer.SelectAPP_QUERY_COLUMNS(predicate);
        }

        public IEnumerable<CLIENT_APPS> GetAllCLIENT_APPS()
        {
            return Context.Consumer.GetAllCLIENT_APPS();
        }

        public CLIENT_APPS SelectCLIENT_APPS(Predicate<CLIENT_APPS> predicate)
        {
            return Context.Consumer.SelectCLIENT_APPS(predicate);
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES()
        {
            return Context.Consumer.GetAllCLIENT_DB_COMPANIES();
        }

        public CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            return Context.Consumer.SelectCLIENT_DB_COMPANIES(predicate);
        }

        public void InsertCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            Context.Consumer.InsertCLIENT_DB_COMPANIES(value);
        }

        public void UpdateCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            Context.Consumer.UpdateCLIENT_DB_COMPANIES(value);
        }

        public void DeleteCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            Context.Consumer.DeleteCLIENT_DB_COMPANIES(predicate);
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS()
        {
            return Context.Consumer.GetAllCLIENT_DB_COMPANY_USERS();
        }

        public CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> predicate)
        {
            return Context.Consumer.SelectCLIENT_DB_COMPANY_USERS(predicate);
        }

        public void InsertCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            Context.Consumer.InsertCLIENT_DB_COMPANY_USERS(value);
        }

        public void UpdateCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            Context.Consumer.UpdateCLIENT_DB_COMPANY_USERS(value);
        }

        public void DeleteCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> value)
        {
            Context.Consumer.DeleteCLIENT_DB_COMPANY_USERS(value);
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            return Context.Consumer.GetAllCLIENT_DB_COMPANY_USER_APPS();
        }

        public CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            return Context.Consumer.SelectCLIENT_DB_COMPANY_USER_APPS(predicate);
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            Context.Consumer.InsertCLIENT_DB_COMPANY_USER_APPS(value);
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            Context.Consumer.UpdateCLIENT_DB_COMPANY_USER_APPS(value);
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            Context.Consumer.DeleteCLIENT_DB_COMPANY_USER_APPS(predicate);
        }

        public IEnumerable<CLIENT_USERS> GetAllCLIENT_USERS()
        {
            return Context.Consumer.GetAllCLIENT_USERS();
        }

        public CLIENT_USERS SelectCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            return Context.Consumer.SelectCLIENT_USERS(predicate);
        }

        public void InsertCLIENT_USERS(CLIENT_USERS value)
        {
            Context.Consumer.InsertCLIENT_USERS(value);
        }

        public void UpdateCLIENT_USERS(CLIENT_USERS value)
        {
            Context.Consumer.UpdateCLIENT_USERS(value);
        }

        public void DeleteCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            Context.Consumer.DeleteCLIENT_USERS(predicate);
        }

        public IEnumerable<LOGS> GetAllLOGS()
        {
            return Context.Consumer.GetAllLOGS();
        }

        public LOGS SelectLOGS(Predicate<LOGS> predicate)
        {
            return Context.Consumer.SelectLOGS(predicate);
        }

        public void InsertLOGS(LOGS value)
        {
            Context.Consumer.InsertLOGS(value);
        }
    }
}
