using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Plex.MobileHub.Data
{
    public static class IDbCommandExtensions
    {
        public static IDbDataParameter CreateParameter(this IDbCommand Command, object Value)
        {
            var Param = Command.CreateParameter();
            Param.Value = Value;
            return Param;
        }
    }
}
