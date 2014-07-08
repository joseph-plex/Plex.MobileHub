using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract(CallbackContract=(typeof(IClientCallback)))]
    public interface IClientService : IDisposable
    {
        IClientCallback ClientCallback { get; }

        [OperationContract(IsOneWay = true)]
        void LogIn(Int32 ClientId, String ClientKey);

        [OperationContract(IsOneWay = true)]
        void LogOut();
        
        #region Access to Pmh Database Data 
        [OperationContract]
        void SelectAPPS();
        [OperationContract]
        void GetAllAPPS();

        [OperationContract]
        void GetAllAPP_QUERIES();
        [OperationContract]
        void SelectAPP_QUERIES();


        [OperationContract]
        void GetAllAPP_QUERY_COLUMNS();
        [OperationContract]
        void SelectAPP_QUERY_COLUMNS();

        [OperationContract]
        void GetAllCLIENT_APPS();
        [OperationContract]
        void SelectCLIENT_APPS();



        [OperationContract]
        void GetAllCLIENT_DB_COMPANIES();
        [OperationContract]
        void SelectCLIENT_DB_COMPANIES();
        [OperationContract]
        void InsertCLIENT_DB_COMPANIES();
        [OperationContract]
        void UpdateCLIENT_DB_COMPANIES();
        [OperationContract]
        void DeleteCLIENT_DB_COMPANIES();


        [OperationContract]
        void GetAllCLIENT_DB_COMPANY_USERS();
        [OperationContract]
        void SelectCLIENT_DB_COMPANY_USERS();
        [OperationContract]
        void InsertCLIENT_DB_COMPANY_USERS();
        [OperationContract]
        void UpdateCLIENT_DB_COMPANY_USERS();
        [OperationContract]
        void DeleteCLIENT_DB_COMPANY_USERS();


        [OperationContract]
        void GetAllCLIENT_DB_COMPANY_USER_APPS();
        [OperationContract]
        void InsertCLIENT_DB_COMPANY_USER_APPS();
        [OperationContract]
        void SelectCLIENT_DB_COMPANY_USER_APPS();
        [OperationContract]
        void UpdateCLIENT_DB_COMPANY_USER_APPS();
        [OperationContract]
        void DeleteCLIENT_DB_COMPANY_USER_APPS();


        [OperationContract]
        void GetAllCLIENT_USERS();
        [OperationContract]
        void InsertCLIENT_USERS();
        [OperationContract]
        void SelectCLIENT_USERS();
        [OperationContract]
        void UpdateCLIENT_USERS();
        [OperationContract]
        void DeleteCLIENT_USERS();

        [OperationContract]
        void GetAllLOGS();
        [OperationContract]
        void SelectLOGS();
        [OperationContract]
        void InsertLOGS();
        #endregion
    }
}
