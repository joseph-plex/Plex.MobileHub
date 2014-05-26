using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Manager.ManagementWebservice;
namespace Plex.MobileHub.Manager.Data
{
    public class Column
    {
        public string ColumnName
        {
            get;
            set;
        }
        public int? ColumnSequence
        {
            get;
            set;
        }
        public string DataType
        {
            get;
            set;
        }
        public int? DataLength
        {
            get;
            set;
        }
        public int? DataPrecision
        {
            get;
            set;
        }
        public int? DataScale
        {
            get;
            set;
        }
        public bool? AllowDbNull
        {
            get;
            set;
        }
        public bool? IsReadOnly
        {
            get;
            set;
        }
        public bool? IsLong
        {
            get;
            set;
        }
        public bool? IsKey
        {
            get;
            set;
        }
        public string KeyType
        {
            get;
            set;
        }
        public bool? IsUnique
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        public static Col FromColumn(Column column)
        {
            if (column == null) 
                throw new ArgumentException("Input cannot be null");

            return new Col()
            {
                ColumnName = column.ColumnName,
                ColumnSequence = column.ColumnSequence,
                DataType = column.DataType,
                DataLength = column.DataLength,
                DataPrecision = column.DataPrecision,
                DataScale = column.DataScale,
                AllowDbNull = column.AllowDbNull,
                IsReadOnly = column.IsReadOnly,
                IsLong = column.IsLong,
                IsKey = column.IsKey,
                KeyType = column.KeyType,
                IsUnique = column.IsUnique,
                Description = column.Description
            };
        }

        public static Column ToColumn(Col column)
        {
            if (column == null)
                throw new ArgumentException("Input cannot be null");

            return new Column()
            {
                ColumnName = column.ColumnName,
                ColumnSequence = column.ColumnSequence,
                DataType = column.DataType,
                DataLength = column.DataLength,
                DataPrecision = column.DataPrecision,
                DataScale = column.DataScale,
                AllowDbNull = column.AllowDbNull,
                IsReadOnly = column.IsReadOnly,
                IsLong = column.IsLong,
                IsKey = column.IsKey,
                KeyType = column.KeyType,
                IsUnique = column.IsUnique,
                Description = column.Description
            };
        }
    }
}
