using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection Conn, string CommandText, params object[] Parameters)
        {
            var Command = Conn.CreateCommand();
            Command.CommandText = CommandText;

            foreach (var p in Parameters)
            {
                var Param =  Command.CreateParameter();
                Param.Value = p;
                Command.Parameters.Add(Param);
            }
            return Command;
        }
    }
}