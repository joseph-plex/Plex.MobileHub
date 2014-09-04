using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Pmh.ServiceLibrary.Types;
namespace Pmh.Client
{
    [ServiceContract]
    interface IClientCommandService
    {
        [OperationContract]
        void LogIn(int id, string key);

        [OperationContract]
        void LogOff();

        [OperationContract]
        IEnumerable<APPS> GetAllAPPS();
        [OperationContract]
        APPS SelectAPPS(Predicate<APPS> predicate);


        [OperationContract]
        IEnumerable<APP_QUERIES> GetAllAPP_QUERIES();
        [OperationContract]
        APP_QUERIES SelectAPP_QUERIES(Predicate<APP_QUERIES> predicate);


        [OperationContract]
        IEnumerable<APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS();
        [OperationContract]
        APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<APP_QUERY_COLUMNS> predicate);


        [OperationContract]
        IEnumerable<CLIENT_APPS> GetAllCLIENT_APPS();
        [OperationContract]
        CLIENT_APPS SelectCLIENT_APPS(Predicate<CLIENT_APPS> predicate);


        [OperationContract]
        IEnumerable<CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES();
        [OperationContract]
        CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate);
        [OperationContract]
        void InsertCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value);
        [OperationContract]
        void UpdateCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value);
        [OperationContract]
        void DeleteCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate);


        [OperationContract]
        IEnumerable<CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS();
        [OperationContract]
        CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> predicate);
        [OperationContract]
        void InsertCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value);
        [OperationContract]
        void UpdateCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value);
        [OperationContract]
        void DeleteCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> value);


        [OperationContract]
        IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS();
        [OperationContract]
        CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate);
        [OperationContract]
        void InsertCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value);
        [OperationContract]
        void UpdateCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value);
        [OperationContract]
        void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate);


        [OperationContract]
        IEnumerable<CLIENT_USERS> GetAllCLIENT_USERS();
        [OperationContract]
        CLIENT_USERS SelectCLIENT_USERS(Predicate<CLIENT_USERS> predicate);
        [OperationContract]
        void InsertCLIENT_USERS(CLIENT_USERS value);
        [OperationContract]
        void UpdateCLIENT_USERS(CLIENT_USERS value);
        [OperationContract]
        void DeleteCLIENT_USERS(Predicate<CLIENT_USERS> predicate);

        [OperationContract]
        IEnumerable<LOGS> GetAllLOGS();
        [OperationContract]
        LOGS SelectLOGS(Predicate<LOGS> predicate);
        [OperationContract]
        void InsertLOGS(LOGS value);

    }
}