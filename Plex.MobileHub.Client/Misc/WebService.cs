using System;
using System.Linq;
using MobileHubClient.PMH;
using System.Collections.Generic;

namespace MobileHubClient.Misc
{
    public static class WebService
    {
        public static List<Command> GetCommands(int connectionId)
        {
            using (Service)
                return Service.GetCommands(connectionId).ToList();
        }

        public static int LogOn(int id, string key, string address, int port)
        {
            using (Service)
                return Service.Login(id, key, address, port);
        }

        public static void LogOff(int connectionId)
        {
            using (Service)
                Service.Logout(connectionId);
        }

        public static void Respond(Response response)
        {
            using (Service)
                Service.Respond(response);
        }

        public static void ResponsePartial(ResponseComponent component)
        {
            using (Service)
                Service.ResponsePartial(component);
        }

        public static ClientSynchroData SyncDataGet()
        {
            using (Service)
                return Service.SyncDataGet();
        }

        public static Data.Result Query(string commandText, params object[] arguments)
        {
            using (Service)
            {   
                var ans = Service.Query(commandText, arguments);
                Data.Result r = new Data.Result() { Rows = new List<Data.Row>(), Columns = new List<Data.Col>() };
                foreach(var resultRow in ans.Rows) r.Rows.Add(new Data.Row(){ Values = resultRow.Values.ToList()});
                
                foreach(var resultColumn in ans.Columns)
                    r.Columns.Add(new Data.Col(){
                        AllowDbNull = resultColumn.AllowDbNull,
                        ColumnName = resultColumn.ColumnName,
                        ColumnSequence = resultColumn.ColumnSequence,
                        DataLength = resultColumn.DataLength,
                        DataPrecision = resultColumn.DataPrecision,
                        DataScale = resultColumn.DataScale,
                        DataType = resultColumn.DataType,
                        Description = resultColumn.Description,
                        IsKey = resultColumn.IsKey,
                        IsReadOnly = resultColumn.IsReadOnly,
                        IsLong = resultColumn.IsLong,
                        IsUnique = resultColumn.IsUnique,
                        KeyType = resultColumn.KeyType
                    });
                return r;
            }
        }

        public static ClientSDKSoapClient Service
        {
            get{
                  return new ClientSDKSoapClient("ClientSDKSoap12");
            }
        }

        public static CLIENTS CilentsRetrieve(int id)
        {
            using (Service)
                return Service.ClientsRetrieve(id);
        }

        public static CLIENT_APPS ClientsAppsRetrieve(int id)
        {
            using (Service)
                return Service.ClientsAppsRetrieve(id);
        }

        public static int ClientDbCompanyAdd(int clientId, string companyCode, string connectionString)
        {
            using (Service)
                return Service.AddClientDbCompany(clientId, companyCode, connectionString);
        }

        public static void ClientDbCompanyRemove(int id)
        {
            using (Service)
                Service.ClientDbCompanyRemove(id);
        }

        public static int ClientDbCompanyUserAdd(int dbCompanyId, int userId, string connectAs = null)
        {
            using (Service)
                return Service.ClientDbCompanyUserAdd(dbCompanyId, userId, connectAs);
        }

        public static void ClientDbCompanyUserRemove(int id)
        {

            using (Service)
                Service.ClientDbCompanyUserRemove(id);
        }

        public static int ClientDbCompanyUserAppsAdd(int appId, int dbCompanyUserId, int? appUserTypeId = null)
        {
            using (Service)
                return Service.ClientDbCompanyUserAppsAdd(appId, dbCompanyUserId, appUserTypeId);
        }

        public static void ClientUserAdd(int clientId, string name, string password)
        {
            using (Service)
                Service.ClientUserAdd(clientId, name, password);
        }

        public static void ClientUserRemove(int clientUserId)
        {
            using (Service)
                Service.ClientUserRemove(clientUserId);
        }

        public static List<CLIENT_USERS> ClientUserRetrieveAllForClients(int clientId)
        {
            using (Service)
                return Service.ClientUserRetrieveAllForClient(clientId).ToList();
        }

        public static CLIENT_USERS ClientUserRetrieve(int clientUserId)
        {
            using (Service)
                return Service.ClientUserRetrieve(clientUserId);
        }

        public static List<CLIENT_DB_COMPANIES> ClientDbCompaniesRetrieve(int connectionId)
        {
            using (Service)
                return Service.ClientDbCompaniesRetrieve(connectionId).ToList();
        }
    }
}
