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


        public static QryResult NonQuery(string commandText, params object[] arguments)
        {
            throw new NotImplementedException();
        }


        public static ClientSDKSoapClient Service
        
        {
            get{
                  return new ClientSDKSoapClient("ClientSDKSoap12");
            }
        }
    }
}
