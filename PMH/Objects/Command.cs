using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Objects;
namespace MobileHub.Objects
{
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