using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class DEV_DATA_VER_QUERIES : RepositoryEntryBase, IRepositoryEntry
    {
        public int DATABASE_VERSION_ID { get; set; }//DATABASE_VERSION_ID	NUMBER(10)	N
        public int QUERY_ID { get; set; }//QUERY_ID	NUMBER(10)	N		
        public DateTime QUERY_EXECUTION_TIME { get; set; }//QUERY_EXECUTION_TIME	DATE	N

        public DEV_DATA_VER_QUERIES() : base() 
        {
            primaryKeys.Add("DATABASE_VERSION_ID");
            primaryKeys.Add("QUERY_ID");

        }
        public DEV_DATA_VER_QUERIES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
