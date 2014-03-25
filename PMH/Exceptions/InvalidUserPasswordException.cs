using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Exceptions
{
    public class InvalidUserPasswordException : PlexError
    {
        public InvalidUserPasswordException(): base ("The Password Specified for this user is incorrect"){}
    }
}