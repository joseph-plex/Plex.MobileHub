using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Manager.ManagementWebservice;

namespace Plex.MobileHub.Manager.Data
{
    public class DataFactory
    {
        public Result Query(string commandText, params object[] arguments)
        {
            return Result.FromQueryResult(GetService().Query(commandText, arguments));
        }

        //public Int32 NonQuery(string commandText, params object [] arguments)
        //{
        //    return GetService().NonQuery(commandText, arguments);
        //}

        public IEnumerable<Consumer> GetConsumer()
        {
            return GetService().GetConsumerRepository();
        }

        public IEnumerable<Command> GetCommands()
        {
            return GetService().GetCommandRepo();
        }

        public IEnumerable<Log> GetLogs()
        {
            return GetService().GetLogRepository();
        }

        public int AppAdd(string authKey, string title, string description, bool isClientCustomApp)
        {
            using (var service = GetService())
                return service.AppAdd(authKey, title, description, isClientCustomApp);
        }

        public void AppRemove(int id)
        {
            using (var service = GetService())
                service.AppRemove(id);
        }

        public int AppTableAdd(int appId, string name, string desc, int insert, int update, int delete, int select)
        {
            using (var service = GetService())
                return service.AppTableAdd(appId, name, desc, insert, update, delete, select);
        }
        public void AppTableRemove(int id)
        {
            using (var service = GetService())
                service.AppTableRemove(id);
        }

        public int AppTableColumnAdd(int tabId, string nom, int seq, string type, int length, int prec, int scale, int nullable, int readOnly, int isLong, int isKey, string keyType, int unique, string desc) 
        {
            using (var service = GetService())
                return service.AppTableColumnAdd(tabId, nom, seq, type, length, prec, scale, nullable, readOnly, isLong, isKey, keyType, unique, desc);
        }
        public void AppTableColumnRemove(int id) 
        {
            using (var service = GetService())
                service.AppTableColumnRemove(id);
        }

        public int ClientAdd(int clientId, string desc, string key) 
        {
            using (var service = GetService())
                return service.ClientAdd(clientId, desc, key);
        }
        public void ClientRemove(int id)
        {
            using (var service = GetService())
                service.ClientRemove(id);
        }


        public void ClientAppRemove(int id) 
        {
            using (var service = GetService())
                service.ClientAppRemove(id);
        }

        public int ClientAppAdd(int appId, int clientId)
        {
            using (var service = GetService())
                return service.ClientAppsAdd(appId, clientId);
        }

       
        ManagerSDKSoapClient GetService()
        {
            return new ManagerSDKSoapClient("ManagerSDKSoap12");
        }
        
    }
}
