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
            try 
            { 
                using (var file = File.Create(FileName))
                    new XmlSerializer(Instance.GetType()).Serialize(file, Instance);
            }
            catch(Exception e)
            {
                ClientService.Logs.Add(e.ToString());
            }
        }
        public static void Load()
        {
            if(File.Exists(FileName))
                using (var file = File.Open(FileName, FileMode.Open))
                    instance = (ClientSettings)new XmlSerializer(Instance.GetType()).Deserialize(file);
         
        }
        public static void Reset()
        {
            instance = new ClientSettings();
        }

        [XmlIgnore]
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
        public List<KeyValuePair<String,String>> DbConnections
        {
            get;
            set;
        }

        int port;
        int clientId;
        string address;
        bool autoLogOn;
        string clientKey;
        int checkInTimer;
        bool autoSaveSettings;

        public IDbConnection GetOpenConnectionByCode(string companyCode)
        {
            foreach(var kvp in DbConnections.Where(p => p.Key == companyCode))
               return new Oracle.DataAccess.Client.OracleConnection(kvp.Value).GetOpenConnection();
            throw new Exception("No Connection Available");
        }
    
        public string ToString(string value)
        {

            if(value.Equals("x",StringComparison.OrdinalIgnoreCase))
            {
                var stringWriter = new StringWriter();
                new XmlSerializer(GetType()).Serialize(stringWriter, this);
                return stringWriter.ToString();
            }
            return base.ToString();
        }

        private ClientSettings() 
        {
            clientKey = String.Empty;
            clientId = 0;
            address = String.Empty;
            port = 0;
            autoLogOn = false;
            checkInTimer = 180000;
            autoSaveSettings = false;

            OnChange += (s, e) =>{
                if (autoSaveSettings)
                    Save();
            };

            DbConnections = new List<KeyValuePair<String, String>>();
        }
        static ClientSettings() { Load(); }
    }
}
