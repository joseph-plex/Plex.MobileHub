using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Pmh.Manager.ManagementResources;

namespace Plex.Pmh.Manager
{
    static class WebService
    {
        public static List<Apps> AppsGet()
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.AppsGet().ToList();
        }

        public static List<Clients> ClientsGet()
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.ClientsGet().ToList();
        }

        public static bool ClientCreate(Clients client)
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.ClientCreate(client);
        }

        public static bool ClientDelete(Clients client)
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.ClientDelete(client);
        }

        public static List<Logs> LogsGet()
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.LogsGet().ToList();
        }

        public static bool AppCreate(Apps app)
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.AppCreate(app);
        }

        public static bool AppDelete(Apps app)
        {
            using (var ws = new ManagerSDKSoapClient("ManagerSDKSoap"))
                return ws.AppDelete(app);
        }

    }
}
