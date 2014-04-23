using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Plex.MobileHub.Devel.DevelService;
using Plex.MobileHub.Devel.Types;

namespace Plex.MobileHub.Devel.Logic
{
    //todo Make all methods static
    public sealed partial class DevelManager
    {
        public static DevelManager Instance
        {
            get
            {
                return instance;
            }
        }
        static DevelManager instance = new DevelManager();
        private DevelManager()
        {
        }
        //events
        public event EventHandler RepositoryModified;

        //project specific variables
        int ApplicationId;
        string ApplicationKey;
        public bool DataLoaded
        {
            get
            {
                return (Repo != null)? true : false;
            }
        }

        DeveloperRepository Repo
        {
            get
            {
                return repo;
            }
            set
            {
                repo = value;
            }
        }

        DeveloperRepository repo;
        public APPS App
        {
            get
            {
                return Instance.Repo.ApplicationData.App;
            }
        }
        public IEnumerable<APP_QUERIES> AppQueries
        {
            get
            {
                return (Instance.Repo == null) ? null : (Instance.Repo.ApplicationData ?? new DevelSyncData()).AppQueries;
            }
        }
        public IEnumerable<APP_TABLES> AppTables
        {
            get
            {
                return (Instance.Repo == null) ? null : (Instance.Repo.ApplicationData ?? new DevelSyncData()).AppTables;
            }
        }
        public IEnumerable<APP_USER_TYPES> AppUserTypes
        {
            get
            {
                return Instance.Repo.ApplicationData.AppUserTypes;
            }
        }
        public IEnumerable<APP_TABLE_COLUMNS> AppTabColumns
        {
            get
            {
                return Instance.Repo.ApplicationData.AppTabColumns;
            }
        }
        public IEnumerable<APP_QUERY_COLUMNS> AppQryColumns
        {
            get
            {
                return Instance.Repo.ApplicationData.AppQryColumns;
            }
        }
        public IEnumerable<APP_QUERY_CONDITIONS> AppQryConditions
        {
            get
            {
                return Instance.Repo.ApplicationData.AppQryConditions;
            }
        }
        public IEnumerable<APP_TABLE_COLUMNS> GetAppTableColumns(int TableId)
        {
            return AppTabColumns.Where(p => p.TABLE_ID == TableId);
        }
        public IEnumerable<APP_QUERY_COLUMNS> GetAppQueryColumns(int QueryId)
        {
            return AppQryColumns.Where(p => p.QUERY_ID == QueryId);
        }
        public IEnumerable<APP_QUERY_CONDITIONS> GetAppQueryConditions(int QueryId)
        {
            return AppQryConditions.Where(p => p.QUERY_ID == QueryId);
        }

        public bool CreateQuery(QueryDefinition definition)
        {
            using (var Service = GetService())
                return Service.QryCreate(ApplicationId, ApplicationKey, definition);
        }
        public void ChangeProject(int id, string key)
        {
            ApplicationId = id;
            ApplicationKey = key;
            
            Repo = new DeveloperRepository(id, key);
            Repo.SynchronizationComplete += Repo_SynchronizationComplete;
            Repo_SynchronizationComplete(this, EventArgs.Empty);
        }
        public void CloseProject()
        {
            Repo = null;
            ApplicationId = 0;
            ApplicationKey = null;
            Repo_SynchronizationComplete(this, EventArgs.Empty);
        }
        public void RequestSync()
        {
            Repo.Sync();
        }
        public void RequestAsyncSync()
        {
            Repo.SyncAsync();
        }

        void Repo_SynchronizationComplete(object sender, EventArgs e)
        {
            if (RepositoryModified != null)
                RepositoryModified(this, EventArgs.Empty);
        }
      
        DevelopersSoapClient GetService()
        {
            return new DevelopersSoapClient("DevelopersSoap");
        }
    }
}
