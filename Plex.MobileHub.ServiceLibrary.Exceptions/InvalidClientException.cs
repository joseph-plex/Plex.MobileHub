using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibrary.Exceptions
{
    public class InvalidClientException : PlexException
    {
        public override string GetMessage()
        {
            return "The Client Specified is Invalid. It may not exist or access to it may have been denied.";
        }
    }
}
