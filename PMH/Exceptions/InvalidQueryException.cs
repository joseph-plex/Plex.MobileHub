using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Exceptions
{
    public class InvalidQueryException : PlexError
    {
        public InvalidQueryException() : base("The query you have specified is invalid") { }
    }
}