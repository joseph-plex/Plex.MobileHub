using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Exceptions
{
    public class MultipleClientLoginException : PlexError
    {
        public MultipleClientLoginException() : base("Another Client with the same Credentials has already logged on")
        { }
    }
}