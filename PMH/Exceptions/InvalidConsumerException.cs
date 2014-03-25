using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Exceptions
{
    public class InvalidConsumerException : PlexError
    {
        public InvalidConsumerException() : base("The connection Id that you have is invalid(It may have expired or have never existed)") { }
    }
}