using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Repositories
{
    public class Commands
    {
        private static Commands commands = new Commands();
        public static Commands Instance
        {
            get
            {
                return commands;
            }
        }

        public static int GetKey()
        {
            int Key;
            var r = new Random();

            do Key = r.Next();
            while (Instance.CommandRepo.ContainsKey(Key));
            return Key;

        }

        public Dictionary<int, Command> CommandRepo = new Dictionary<int, Command>();

        public event Subscriber OnAdd;
        public event Subscriber OnModify;
        public event Subscriber OnDelete;

        public Dictionary<int, Command> GetAll()
        {
            return new Dictionary<int, Command>(CommandRepo);
        }

        public int Add(Command Comm)
        {
            CommandRepo.Add(Comm.RequestId, Comm);
            if(OnAdd!=null) OnAdd(Comm, EventArgs.Empty);
            return Comm.RequestId;
        }
       
        public int Add(int ClientId, string Name, List<Object> Params)
        {
            return Add(new Command() { RequestId = GetKey(), Name = Name, ClientId = ClientId, Params = Params });
        }
       
        public void Add(List<int> ClientIds, string Name, List<Object> Params)
        {
            foreach (int id in ClientIds)
                Add(id, Name, Params);
        }

        public void Modify(int ConnectionId, Command Data)
        {
            CommandRepo[ConnectionId] = Data;
            if(OnModify!=null)OnModify(CommandRepo[ConnectionId], EventArgs.Empty);

        }

        public void Remove(int i)
        {
            if(OnDelete!= null) OnDelete(CommandRepo[i], EventArgs.Empty);
            CommandRepo.Remove(i);
        }

        public Command Retrieve(int i)
        {
            return CommandRepo[i];
        }

        public List<Command> GetCommands(int ClientId)
        {
            return GetAll().Values.ToList().FindAll((p) => p.ClientId == ClientId);
        }
      

        void OnDBAccessInsert(object sender, EventArgs e)
        {
            SyncAllClientDatabases();
        }

        public void SyncAllClientDatabases()
        {
            List<int> ClientIds = new List<int>();
            foreach (var con in Connections.Instance.GetAll())
                ClientIds.Add(con.Value.ClientId);
            Add(ClientIds, "Sync", new List<object>());
        } 
        
    }
}