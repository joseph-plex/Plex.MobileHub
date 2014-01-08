using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Repositories;

namespace Plex.PMH.Objects
{
    public class GetCommandsMethodResult : MethodResult
    {
        public List<Command> Commands = new List<Command>();
    }
}