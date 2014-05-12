using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using MobileHubClient.Data;
using Plex.Logs;
using System.ComponentModel;
using System.Data;
namespace MobileHubClient.Core
{
    public class ClientSettings
    {
        public const string FileName = "MobileHub.Client.Settings.xml";
        public static ClientSettings Instance { get { return instance; } }
        static ClientSettings instance = new ClientSettings();

        public static void Save()
        {
            try { 
                using (var file = File.Create(FileName))
                    new XmlSerializer(Instance.GetType()).Serialize(file, Instance);
            }
            catch(Exception e)
            {
                LogManager.Instance.Add(e.ToString());
            }
        }
        public static void Load()
        {
            try { 
                using (var file = File.Open(FileName, FileMode.Open))
                    instance = (ClientSettings)new XmlSerializer(Instance.GetType()).Deserialize(file);
            }
            catch(FileNotFoundException) {  
                LogManager.Instance.Add(FileName + " has not been found. Loading ClientSettings Failed");
            }
        }
        public static void Reset()
        {
            instance = new ClientSettings();
        }

        public PropertyChangedEventHandler OnChange;

        public String ClientKey
        {
            get
            {
                return clientKey;
            }
            set{
                OnChange(this, new PropertyChangedEventArgs("ClientKey"));
                clientKey = value;
            }
        }
        public Int32 ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("ClientId"));
                clientId = value;
            }
        }
        public String Address
        {
            get
            {
                return address;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("Address"));
                address = value;
            }
        }
        public Int32 Port
        {
            get
            {
                return port;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("Port"));
                port = value;
            }
        }
        public Boolean AutoLogOn
        {
            get
            {
                return autoLogOn;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("AutoLogOn"));
                autoLogOn = value;
            }
        }
        public Int32 CheckInTimer
        {
            get
            {
                return checkInTimer;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("CheckInTimer"));
                checkInTimer = value;
            }
        }
        public bool AutoSaveSettings
        {

            get
            {
                return autoSaveSettings;
            }
            set
            {
                OnChange(this, new PropertyChangedEventArgs("AutoSaveSettings"));
                autoSaveSettings = value;
            }


        }
        public List<DbConnectionData> DbConnections
        {
            get;
            set;
        }

        string clientKey;
        int clientId;
        string address;
        int port;
        bool autoLogOn;
        int checkInTimer;
        bool autoSaveSettings;

        public IDbConnection GetOpenConnectionByCode(string companyCode)
        {
            var ConnectionData = DbConnections.FirstOrDefault(p => companyCode == p.Company);
            if (ConnectionData == null) 
                throw new Exception("Company Code does not exist within the system");
            return ConnectionData.GetOpenConnection();
        }
    
        public List<DbConnectionData> RepairDbConnectionCollection()
        {
            var list = new List<DbConnectionData>();
            lock (DbConnections)
            {
                foreach(var item in DbConnections)
                {
                    //Search for duplicate company names;
                    var element = list.FirstOrDefault(p => p.Company == item.Company);

                    // list elements must have : unique non empty non null company name and more than one connection strings
                    if ((element == null) && ((item.Company ?? string.Empty) != string.Empty) && (item.ConnectionStrings.Count != 0))
                        list.Add(item);
                    
                    //if Element does exist already
                    if (element != null) //if Any of the connection strings do not current exist, add it.
                        item.ConnectionStrings.ForEach(p => { 
                            if (!element.ConnectionStrings.Contains(p)) 
                                element.ConnectionStrings.Add(p); 
                        });
                }
                //Replace the old broken version with the new version
                return DbConnections = list;
            }
        }

        private ClientSettings() 
        {
            clientKey = String.Empty;
            clientId = 0;
            port = 0;
            address = String.Empty;
            autoLogOn = false;
            checkInTimer = 180000;

            OnChange += (s, e) =>{
                if (autoSaveSettings)
                    Save();
            };

            DbConnections = new List<DbConnectionData>();
        }
        static ClientSettings() { Load(); }
    }
}
