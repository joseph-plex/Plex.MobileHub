using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;

namespace MobileHub.Objects.ResultTypes
{
    public class GetCommandsMethodResult : MethodResult
    {
        public List<Command> Commands = new List<Command>();
    }
}