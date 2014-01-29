using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Plex.PMH.Exceptions
{
    public class PlexError : Exception
    {
        public int ErrorCode;

        public PlexError()
            : base() { }
        public PlexError(string message)
            : base(message) { }
        public PlexError(string message, Exception innerException) 
            : base (message,innerException) { }
    }
}