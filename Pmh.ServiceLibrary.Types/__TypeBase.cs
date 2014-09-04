using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Runtime.Serialization;
using Pmh.ServiceLibrary.Types;
namespace Pmh.ServiceLibrary.Types
{
    [DataContract]
    [KnownType(typeof(APP_QUERIES))]
    [KnownType(typeof(APP_QUERY_COLUMNS))]
    [KnownType(typeof(APP_QUERY_CONDITIONS))]
    [KnownType(typeof(APP_TABLE_COLUMNS))]

    [KnownType(typeof(APP_TABLES))]
    [KnownType(typeof(APP_USER_TYPES))]
    [KnownType(typeof(APPS))]
    [KnownType(typeof(CLIENT_APPS))]

    [KnownType(typeof(CLIENT_DB_COMPANIES))]
    [KnownType(typeof(CLIENT_DB_COMPANY_USER_APPS))]
    [KnownType(typeof(CLIENT_DB_COMPANY_USERS))]
    [KnownType(typeof(CLIENT_USERS))]

    [KnownType(typeof(CLIENTS))]
    [KnownType(typeof(DEV_DATA))]
    [KnownType(typeof(DEV_DATA_VER))]
    [KnownType(typeof(DEV_DATA_VER_QUERIES))]

    [KnownType(typeof(DEVICES))]
    [KnownType(typeof(LOGS))]
    [KnownType(typeof(PMH_SETTINGS))]
    [KnownType(typeof(QUERY_SEQUENCE_REQUESTS))]


    public abstract class __TypeBase 
        : RepositoryEntryBase, IRepositoryEntry
    {

    }
}
