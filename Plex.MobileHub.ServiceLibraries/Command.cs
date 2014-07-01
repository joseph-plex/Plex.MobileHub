using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries
{
    public class Command
    {
        public String Name;
        public String ClientId;
        public List<Object> Params { get; set; }

        public Command()
        {

        }
    }
}
