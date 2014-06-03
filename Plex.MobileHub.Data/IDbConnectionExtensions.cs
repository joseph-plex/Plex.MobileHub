using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Plex.MobileHub.Data
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection Conn, string CommandText, params object[] Parameters)
        {
            var Command = Conn.CreateCommand();
            Command.CommandText = CommandText;
            foreach (var p in Parameters ?? new object[0])
                Command.Parameters.Add(Command.CreateParameter(p));
            return Command;
        }


        public static PlexResult Query(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            PlexResult r = new PlexResult();
            using (IDbCommand Comm = conn.CreateCommand(CommandText, Arguments))
            using (IDataReader reader = Comm.ExecuteReader(CommandBehavior.KeyInfo))
               return new PlexResult(reader);
            
        }



        public static int NonQuery(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            using (var Comm = conn.CreateCommand(CommandText, Arguments))
                return Comm.ExecuteNonQuery();
        }
    }
}
