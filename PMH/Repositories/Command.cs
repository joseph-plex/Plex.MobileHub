using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.PMH.Objects;
namespace Plex.PMH.Repositories
{
    public static class CommandFactory
    {
        const string SyncComm = "Sync";
        const string IUDComm = "IUD";
        public static Command CreateCommand(ClientCommandType CommandType)
        {
            switch(CommandType)
            {
                case ClientCommandType.Query:
                    return new Command();
                default:
                    throw new NotImplementedException();
            }
        }

        public static Command CreateSyncCommand()
        {
            Command command = new Command();
            command.Name = SyncComm;
            return command;
        }
        public static Command CreateSyncCommand(int ClientId)
        {
            var command = CreateSyncCommand();
            command.ClientId = ClientId;
            return command;
        }
     
        public static Command CreateIUDCommand(int ClientId, string DBCode, IUDData ChangeRequest)
        {
            var command = CreateIUDCommand(ClientId);
            command.Params.Add(DBCode);
            command.Params.Add(ChangeRequest);
            return command;
        }
        public static Command CreateIUDCommand(int ClientId)
        {
            var command = CreateIUDCommand();
            command.ClientId = ClientId;
            return command;
        }
        public static Command CreateIUDCommand()
        {
            Command command = new Command();
            command.Name = IUDComm;
            return command;
        }
    }

    public class Command
    {
        public const string Query = "Query";
        public const string ExecuteRegisteredQuery = "ExecuteRegisteredQuery";

        public string Name;
        public int ClientId;
        public int RequestId;
        public List<object> Params;

        public Command()
        {
            Name = String.Empty;
            ClientId = new int();
            RequestId = new int();
            Params = new List<Object>();
        }
    }
    public enum ClientCommandType
    {
        Query,
        ExecuteRegisteredQuery,
    }
}