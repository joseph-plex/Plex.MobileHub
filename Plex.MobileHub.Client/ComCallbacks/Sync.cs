using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

using MobileHubClient.PMH;
using Plex.Logs;
using MobileHubClient.Data;
using MobileHubClient.Misc;
using System.Data;
using MobileHubClient.Core;
namespace MobileHubClient.ComCallbacks
{
    public static partial class Functions
    {
        public static bool Sync()
        {
            try
            {
                ClientService.Logs.Add("Initializing Synchro", LogPriority.Normal);
                var SyncData = WebService.SyncDataGet();
                foreach(var kvp in ClientSettings.Instance.DbConnections)
                    using(var connection = ClientDbConnectionFactory.ActiveConnection(kvp.Value))
                        foreach (var AppData in SyncData.Apps)
                            try { SynchronizeApplicationData(connection, AppData); }
                            catch (Exception e) { ClientService.Logs.Add(e); }
                ClientService.Logs.Add("Synchro Complete", LogPriority.Normal);
            }
            catch (Exception e)
            {
                ClientService.Logs.Add(e);
                return false;
            }
            return true;
        }

        static void SynchronizeApplicationData(IDbConnection Conn, ApplicationSynchroData AppData)
        {
            using (var Transaction = Conn.BeginTransaction())
            {
                string IsAppRegisteredSql = "select * from pmh$_apps where APP_Id=:a";
                string AppRegisteredSql = "insert into pmh$_apps(app_id, title, description) values (:a,:b,:c)";

                if (Conn.Query(IsAppRegisteredSql, AppData.ApplicationId).Rows.Count == 0)
                    Conn.NonQuery(AppRegisteredSql, AppData.ApplicationId, AppData.ApplicationTitle, AppData.ApplicationDescription);

                foreach (var QuerySyncData in AppData.ApplicationQueries)
                {
                    var Q = QuerySyncData;
                    try { 
                        if (Conn.InvokeProcedure<int>("PMH.QueryExists", Q.QueryId) == 0)
                        {
                            var Con = new List<QueryConditionSynchroData>(QuerySyncData.Conds);
                            var Col = new List<QueryColumnSynchroData>(QuerySyncData.Cols);

                            try
                            {
                                Conn.InvokeProcedure<int>("PMH.QueryCreate", AppData.ApplicationId, Q.QueryId, Q.Description, Q.TableName);
                            }
                            catch (Exception e)
                            {
                                ClientService.Logs.Add("Problem With Query " + Q.QueryId + e.ToString(), LogPriority.Highest, true);
                                throw;
                            }

                            try
                            {
                                Parallel.ForEach(Col, (a) => Conn.InvokeProcedure<int>("PMH.QueryColumnCreate", Q.QueryId, a.ColId, a.ColName, a.ColSeq));
                            }
                            catch (AggregateException ae)
                            {
                                foreach (var ex in ae.InnerExceptions)
                                    ClientService.Logs.Add("Problem With Query " + Q.QueryId + ex.ToString(), LogPriority.Highest, true);
                            }

                            try
                            {
                                Parallel.ForEach(Con, (a) => Conn.InvokeProcedure<int>("PMH.QueryConditionCreate", Q.QueryId, a.ColumnName, a.ColumnNVL, a.ColumnValue, a.Operation));
                            }
                            catch (AggregateException ae)
                            {
                                foreach (var ex in ae.InnerExceptions)
                                    ClientService.Logs.Add("Problem With Query " + Q.QueryId + ex.ToString(), LogPriority.Highest, true);
                            }
                            Conn.InvokeProcedure<int>("PMH.QueryCreateSyncTrigger", Q.QueryId);
                            Conn.InvokeProcedure<int>("PMH.QuerySyncData", Q.QueryId);
                        }
                    }
                    catch(Exception ex) {
                        ClientService.Logs.Add("Problem With Query " + Q.QueryId + ex.ToString(), LogPriority.Highest, true);
                    }
                }
                Transaction.Commit();
            }
            

        }
    }
}
