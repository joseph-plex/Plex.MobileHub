using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Exceptions
{
    public class UnauthorizedClientException : PlexError
    {
        public UnauthorizedClientException() : base("The client is not authorized to use this application, please contact a Plexxis administrator") { }
    }
}