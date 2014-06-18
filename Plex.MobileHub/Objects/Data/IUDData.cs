using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Objects
{
    public class IUDData
    {
        /// <summary>
        /// Can be 0 - 3.
        /// 0 means fall back onto row Operation *Not currently supported*
        /// 1 means to insert
        /// 2 means to update
        /// 3 means to delete.
        /// </summary>
        public String TableName { get; set; }
        public List<String> ColumnNames { get; set; }
        public List<Row> Rows { get; set; }

        public IUDData()
        {
            ColumnNames = new List<String>();
            Rows = new List<Row>();
        }
        public bool IsValid()
        {
            //This is required to ensure data ingrety in operations. 
            foreach (var r in Rows)
                if (r.Values.Count != ColumnNames.Count)
                    return false;

            //Columns have a maxmium length of 30 characters
            foreach (var column in ColumnNames)
                if (column.Length > 30)
                    return false;

            //Tables have a maxmium length of 30 characters. 
            if (TableName.Length > 30)
                return false;

            return true;
        }

    }
}