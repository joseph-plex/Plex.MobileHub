using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Exceptions
{
    public class InvalidQueryException : PlexError
    {
        public InvalidQueryException() : base("The query you have specified does not exist") { }
    }
}