using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Objects;

namespace Plex.MobileHub.Repositories
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
        private Commands()
        {
        }

        //void Commands_OnAdd(object sender, EventArgs e)
        //{
        //    var Comm = (Command)sender;
        //    var Client = Connections.Instance.Retrieve(Comm.ClientId);
        //    Client.RequestPull();
        //}

        public static int GetKey()
        {
            int Key;
            var r = new Random();

            do Key = r.Next();
            while (Instance.CommandRepo.ContainsKey(Key));
            return Key;

        }

        public Dictionary<Int32, Command> CommandRepo = new Dictionary<Int32, Command>();

        public event Subscriber OnAdd;
        public event Subscriber OnModify;
        public event Subscriber OnDelete;

        public Dictionary<Int32, Command> GetAll()
        {
            return new Dictionary<Int32, Command>(CommandRepo);
        }

        public int Add(Command Comm)
        {
            CommandRepo.Add(Comm.RequestId, Comm);
            if(OnAdd!=null) OnAdd(Comm, EventArgs.Empty);
            return Comm.RequestId;
        }

        public int Add(Int32 ClientId, string Name, List<Object> Params)
        {
            return Add(new Command() { RequestId = GetKey(), Name = Name, ClientId = ClientId, Params = Params });
        }

        public void Add(List<Int32> ClientIds, String Name, List<Object> Params)
        {
            foreach (Int32 id in ClientIds)
                Add(id, Name, Params);
        }

        public void Modify(Int32 ConnectionId, Command Data)
        {
            CommandRepo[ConnectionId] = Data;
            if(OnModify!=null)OnModify(CommandRepo[ConnectionId], EventArgs.Empty);

        }

        public void Remove(Int32 i)
        {
            if(OnDelete!= null) OnDelete(CommandRepo[i], EventArgs.Empty);
            CommandRepo.Remove(i);
        }

        public Command Retrieve(Int32 i)
        {
            return CommandRepo[i];
        }

        public List<Command> GetCommands(Int32 ClientId)
        {
            return GetAll().Values.ToList().FindAll((p) => p.ClientId == ClientId);
        }
      
        void OnDBAccessInsert(Object sender, EventArgs e)
        {
            SyncAllClientDatabases();
        }

        public void SyncAllClientDatabases()
        {
            List<Int32> ClientIds = new List<Int32>();
            foreach (var con in Connections.Instance.GetAll())
                ClientIds.Add(con.Value.ClientId);
            Add(ClientIds, "Sync", new List<Object>());
        } 
    }
}