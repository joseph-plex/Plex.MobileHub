﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Exceptions
{
    public class InvalidClientException : PlexError
    {
        public InvalidClientException()  : base("The Client you have specified is invalid")
        { }
    }
}