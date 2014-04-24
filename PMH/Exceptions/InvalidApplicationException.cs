using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Exceptions
{
    /// <summary>
    /// This Exception is thrown when the application you have specified does not exist
    /// </summary>
    public class InvalidApplicationException : PlexError
    {
        public InvalidApplicationException(): base("The application you Have specified is invalid"){}
    }
}