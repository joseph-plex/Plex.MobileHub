using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Exceptions
{
    public class InvalidApplicationKeyException : PlexError
    {
        public InvalidApplicationKeyException() : base("The Application Key specified is invalid") { }
    }
}