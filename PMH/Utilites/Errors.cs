using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH
{

    internal class Errors
    {
        public const int UnhandledException = -9999;

        public string this[int ErrorCode]
        {
            get
            {
                return map[ErrorCode];
            }
        }
        private Dictionary<int,string> map = new Dictionary<int,string>();
        public Errors()
        {
            map.Add(UnhandledException,"An Unhandled Error Has Occurred");
        }
    }
    internal class PlexError : Exception
    {
        public int ErrorCode
        {
            get;
            protected set;
        }

        public PlexError(int ErrorCode) : base() { this.ErrorCode = ErrorCode; }
        public PlexError(int ErrorCode, String message) : base(message) { this.ErrorCode = ErrorCode; }
        public PlexError(int ErrorCode, String message, Exception inner) : base(message, inner) { this.ErrorCode = ErrorCode; }
    }
}