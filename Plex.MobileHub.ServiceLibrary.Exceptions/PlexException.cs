using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibrary.Exceptions
{
    public abstract class PlexException : Exception
    {
        public override string Message
        {
            get{
                return GetMessage();
            }
        }
        public abstract String GetMessage();
    }
}
