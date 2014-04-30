using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Objects.Synchronization
{
    public sealed class DevelSyncData
    {

        public static DevelSyncData GetDataForApp(int AppId, string AppKey)
        {

            using(var Conn = Utilities.GetConnection(true))
            { 
                var SyncData = new DevelSyncData();
                SyncData.App = APPS.GetAll(Conn).FirstOrDefault(p => p.APP_ID == AppId);
                if (SyncData.App == null) throw new Exception("Application " + AppId + " does not exist");
                if (SyncData.App.AUTH_KEY != AppKey) throw new Exception("Application Key specified is incorrect");

                SyncData.AppQueries = SyncData.App.GetAPP_QUERIES(Conn).ToList();
                SyncData.AppTables = SyncData.App.GetAPP_TABLES(Conn).ToList();
                SyncData.AppUserTypes = SyncData.App.GetAPP_USER_TYPES(Conn).ToList();

                SyncData.AppQryColumns = new List<APP_QUERY_COLUMNS>();
                SyncData.AppQryConditions = new List<APP_QUERY_CONDITIONS>();
                SyncData.AppTabColumns = new List<APP_TABLE_COLUMNS>();

                foreach (var v in SyncData.AppQueries) { 

                    SyncData.AppQryColumns.AddRange(v.GetAPP_QUERY_COLUMNS(Conn));
                    SyncData.AppQryConditions.AddRange(v.GetAPP_QUERY_CONDITIONS(Conn));
                }

                foreach (var v in SyncData.AppTables)
                    SyncData.AppTabColumns.AddRange(v.GetAPP_TABLE_COLUMNS(Conn));

                return SyncData;
            }
        }


        public APPS App
        {
            get;
            set;
        }

        public List<APP_QUERIES> AppQueries
        {
            get;
            set;
        }

        public List<APP_TABLES> AppTables
        {
            get;
            set;
        }

        public List<APP_USER_TYPES> AppUserTypes
        {
            get;
            set;
        }

        public List<APP_TABLE_COLUMNS> AppTabColumns
        {
            get;
            set;
        }
        public List<APP_QUERY_COLUMNS> AppQryColumns
        {
            get;
            set;
        }
        public List<APP_QUERY_CONDITIONS> AppQryConditions
        {
            get;
            set;
        }
    }
}