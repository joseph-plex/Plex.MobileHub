﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public class RepositoryEntryBase
    {

        public static RepositoryEntryBase FromPlexQueryResultTuple( RepositoryEntryBase reb, PlexQueryResultTuple plexTuple)
        {

            if (plexTuple.parent == null)
                throw new NotSupportedException("This Operation is Not supported by this PlexTuple.");

            Type type = reb.GetType();
            var pInfo = type.GetProperties();
            PlexQueryResult result = plexTuple.parent;

            foreach (var p in pInfo)
            {
                int index = result.Tuples.IndexOf(plexTuple);

                if (result[p.Name, index] == null)
                    continue;
               
                var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                object value = Convert.ChangeType(result[p.Name, index], (result[p.Name, index] != null)?conversationType: p.PropertyType);
                p.SetValue(reb, value);
            }
            return reb;
        }

        protected IList<String> primaryKeys;

        public RepositoryEntryBase() 
        {
            primaryKeys = new List<String>();
        }
        public RepositoryEntryBase(PlexQueryResultTuple plexTuple) : this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }


        public IList<String> GetPrimaryKeys()
        {
            return primaryKeys;
        }
    }
}
