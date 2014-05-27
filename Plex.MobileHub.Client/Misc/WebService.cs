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

        public static QryResult Query(string commandText, params object[] arguments)
        {
            throw new NotImplementedException();
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
