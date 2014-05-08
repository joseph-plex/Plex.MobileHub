using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.Core;

namespace MobileHubClient.Channels
{
    public class PlexServiceBase : IPlexService
    {
        public static ClientService Context
        {
            get;
            set;
        }
    }
}
