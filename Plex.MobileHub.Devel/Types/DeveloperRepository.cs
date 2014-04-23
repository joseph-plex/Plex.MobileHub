using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Devel.DevelService;
namespace Plex.MobileHub.Devel.Types
{
    internal sealed class DeveloperRepository
    {
        public event EventHandler SynchronizationComplete;
    
        int id;
        string key;

        public bool SyncInProgress
        {
            get;
            private set;
        }

        internal DevelSyncData ApplicationData;

        public DeveloperRepository(int Id, string Key)
        {
            id = Id;
            key = Key;
            Sync();
        }

        public void Sync()
        {
            SyncInProgress = true;
            var buffer = Pull();
            
            lock(ApplicationData ?? new DevelSyncData())
                if ((ApplicationData != null) ? !CompareApplicationData(buffer) : true)
                    ApplicationData = buffer;
            
            SyncInProgress = false;
            OnSynchronizationComplete();
        }


        public void SyncAsync()
        {
            Task.Run(() => Sync());
        }

        DevelSyncData Pull()
        {
            using (var Service = GetService())
                return Service.GetAppSyncData(id,key);
        }
        DevelopersSoapClient GetService()
        {
            return new DevelopersSoapClient("DevelopersSoap");
        }
        void OnSynchronizationComplete()
        {
            if (SynchronizationComplete != null)
                SynchronizationComplete(this, EventArgs.Empty);
        }
        bool CompareApplicationData( DevelSyncData ApplicationDataBuffer)
        {
            if (!CompareApps(ApplicationDataBuffer))
                return false;
            if (!CompareQueries(ApplicationDataBuffer))
                return false;
            if (!CompareTables(ApplicationDataBuffer))
                return false;
            if (!CompareTableColumns(ApplicationDataBuffer))
                return false;
            if (!CompareAppQryColumn(ApplicationDataBuffer))
                return false;
            if (!CompareAppQryCondition(ApplicationDataBuffer))
                return false;
            return true;
        }
        bool CompareApps( DevelSyncData ApplicationDataBuffer)
        {
            if (ApplicationData == null && ApplicationDataBuffer == null)
                return true;
            if (ApplicationData != null && ApplicationDataBuffer == null)
                return false;
            if (ApplicationData == null && ApplicationDataBuffer != null)
                return false;

            if (ApplicationData.App == null && ApplicationDataBuffer.App == null)
                return true;
            if (ApplicationData.App != null && ApplicationDataBuffer.App == null)
                return false;
            if (ApplicationData.App == null && ApplicationDataBuffer.App != null)
                return false;

            if (ApplicationData.AppQueries == null && ApplicationDataBuffer.AppQueries == null)
                return true;
            if (ApplicationData.AppQueries != null && ApplicationDataBuffer.AppQueries == null)
                return false;
            if (ApplicationData.AppQueries == null && ApplicationDataBuffer.AppQueries != null)
                return false;

            if (ApplicationData.AppTables == null && ApplicationDataBuffer.AppTables == null)
                return true;
            if (ApplicationData.AppTables != null && ApplicationDataBuffer.AppTables == null)
                return false;
            if (ApplicationData.AppTables == null && ApplicationDataBuffer.AppTables != null)
                return false;

            if (ApplicationData.AppUserTypes == null && ApplicationDataBuffer.AppUserTypes == null)
                return true;
            if (ApplicationData.AppUserTypes != null && ApplicationDataBuffer.AppUserTypes == null)
                return false;
            if (ApplicationData.AppUserTypes == null && ApplicationDataBuffer.AppUserTypes != null)
                return false;

            if (ApplicationData.AppQryColumns == null && ApplicationDataBuffer.AppQryColumns == null)
                return true;
            if (ApplicationData.AppQryColumns != null && ApplicationDataBuffer.AppQryColumns == null)
                return false;
            if (ApplicationData.AppQryColumns == null && ApplicationDataBuffer.AppQryColumns != null)
                return false;

            if (ApplicationData.AppQryConditions == null && ApplicationDataBuffer.AppQryConditions == null)
                return true;
            if (ApplicationData.AppQryConditions != null && ApplicationDataBuffer.AppQryConditions == null)
                return false;
            if (ApplicationData.AppQryConditions == null && ApplicationDataBuffer.AppQryConditions != null)
                return false;

            if (ApplicationData.AppTabColumns == null && ApplicationDataBuffer.AppTabColumns == null)
                return true;
            if (ApplicationData.AppTabColumns != null && ApplicationDataBuffer.AppTabColumns == null)
                return false;
            if (ApplicationData.AppTabColumns == null && ApplicationDataBuffer.AppTabColumns != null)
                return false;


            //Is Application Level Data the same? 
            if (ApplicationData.App.APP_ID != ApplicationDataBuffer.App.APP_ID)
                return false; //ApplicationId is different
            if (ApplicationData.App.AUTH_KEY != ApplicationDataBuffer.App.AUTH_KEY)
                return false; //Application Authentication Key is different
            if (ApplicationData.App.DESCRIPTION != ApplicationDataBuffer.App.DESCRIPTION)
                return false; //Application Description is different
            if (ApplicationData.App.IS_CLIENT_CUSTOM_APP != ApplicationDataBuffer.App.IS_CLIENT_CUSTOM_APP)
                return false; //Application Custom Client Information is Different
            if (ApplicationData.App.TITLE != ApplicationDataBuffer.App.TITLE)
                return false; //Application Data Buffer is different
            return true;
        }
        bool CompareQueries( DevelSyncData ApplicationDataBuffer)
        {
            foreach (var Q in ApplicationDataBuffer.AppQueries)
            {
                var OldQ = ApplicationData.AppQueries.FirstOrDefault(p => p.QUERY_ID == Q.QUERY_ID);

                if (OldQ == null)
                    return false;

                if (Q.APP_ID != OldQ.APP_ID)
                    return false;
                if (Q.DESCRIPTION != OldQ.DESCRIPTION)
                    return false;
                if (Q.IS_DELTA != OldQ.IS_DELTA)
                    return false;
                if (Q.NAME != OldQ.NAME)
                    return false;
                if (Q.SEQ_LIMIT != OldQ.SEQ_LIMIT)
                    return false;
                if (Q.SEQ_LIMIT_TIMESPAN != OldQ.SEQ_LIMIT_TIMESPAN)
                    return false;
                if (Q.SQL != OldQ.SQL)
                    return false;
                if (Q.TABLE_ID != OldQ.TABLE_ID)
                    return false;
                if (Q.TABLE_NAME != OldQ.TABLE_NAME)
                    return false;
            }
            return true;

        }
        bool CompareTables( DevelSyncData ApplicationDataBuffer)
        {
            foreach (var T in ApplicationDataBuffer.AppTables)
            {
                var OldT = ApplicationData.AppTables.FirstOrDefault(p => p.TABLE_ID == T.TABLE_ID);

                if (OldT == null)
                    return false;

                if (OldT.APP_ID != T.APP_ID)
                    return false;
                if (OldT.DELETE_ALLOWED != T.DELETE_ALLOWED)
                    return false;
                if (OldT.DESCRIPTION != T.DESCRIPTION)
                    return false;
                if (OldT.INSERT_ALLOWED != T.INSERT_ALLOWED)
                    return false;
                if (OldT.NAME != T.NAME)
                    return false;
                if (OldT.QUERY_ALLOWED != T.QUERY_ALLOWED)
                    return false;
                if (OldT.UPDATE_ALLOWED != T.UPDATE_ALLOWED)
                    return false;
            }
            return true;
        }
        bool CompareTableColumns( DevelSyncData ApplicationDataBuffer)
        {
            foreach (var T in ApplicationDataBuffer.AppTabColumns)
            {
                var OldT = ApplicationData.AppTabColumns.FirstOrDefault(p => p.TABLE_COLUMN_ID == T.TABLE_COLUMN_ID);

                if (OldT == null)
                    return false;

                if (OldT.ALLOW_DB_NULL != T.ALLOW_DB_NULL)
                    return false;
                if (OldT.COLUMN_NAME != T.COLUMN_NAME)
                    return false;
                if (OldT.COLUMN_SEQUENCE != T.COLUMN_SEQUENCE)
                    return false;
                if (OldT.DATA_LENGTH != T.DATA_LENGTH)
                    return false;
                if (OldT.DATA_PRECISION != T.DATA_PRECISION)
                    return false;
                if (OldT.DATA_SCALE != T.DATA_SCALE)
                    return false;
                if (OldT.DATA_TYPE != T.DATA_TYPE)
                    return false;
                if (OldT.DESCRIPTION != T.DESCRIPTION)
                    return false;
                if (OldT.IS_KEY != T.IS_KEY)
                    return false;
                if (OldT.IS_LONG != T.IS_LONG)
                    return false;
                if (OldT.IS_READ_ONLY != T.IS_READ_ONLY)
                    return false;
                if (OldT.IS_UNIQUE != T.IS_UNIQUE)
                    return false;
                if (OldT.KEY_TYPE != T.KEY_TYPE)
                    return false;
                if (OldT.TABLE_ID != T.TABLE_ID)
                    return false;
            }
            return true;
        }
        bool CompareAppQryColumn( DevelSyncData ApplicationDataBuffer)
        {
            if (ApplicationDataBuffer.AppQryColumns.Length != ApplicationData.AppQryColumns.Length)
                return false;

            foreach (var C in ApplicationData.AppQryColumns)
            {
                var OldC = ApplicationDataBuffer.AppQryColumns.FirstOrDefault(p => p.COLUMN_ID == C.COLUMN_ID);

                if (OldC == null)
                    return false;

                if (C.COLUMN_NAME != OldC.COLUMN_NAME)
                    return false;
                if (C.COLUMN_SEQUENCE != OldC.COLUMN_SEQUENCE)
                    return false;
                if (C.QUERY_ID != OldC.QUERY_ID)
                    return false;
            }
            return true;
        }
        bool CompareAppQryCondition( DevelSyncData ApplicationDataBuffer)
        {
            if (ApplicationDataBuffer.AppQryConditions.Length != ApplicationData.AppQryConditions.Length)
                return false;

            foreach (var C in ApplicationData.AppQryConditions)
            {
                var OldC = ApplicationDataBuffer.AppQryConditions.FirstOrDefault(p => p.CONDITION_ID == C.CONDITION_ID);

                if (OldC == null)
                    return false;

                if (C.COLUMN_NAME != OldC.COLUMN_NAME)
                    return false;
                if (C.COLUMN_NVL != OldC.COLUMN_NVL)
                    return false;
                if (C.COLUMN_VALUE != OldC.COLUMN_VALUE)
                    return false;
                if (C.OPERATOR != OldC.OPERATOR)
                    return false;
                if (C.QUERY_ID != OldC.QUERY_ID)
                    return false;
            }
            return true;
        }
    }
}
