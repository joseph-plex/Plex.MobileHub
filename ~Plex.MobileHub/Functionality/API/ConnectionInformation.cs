using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Functionality.API
{
    public class ConnectionInformation : FunctionStrategyBase<MethodResult>
    {

        public IRepository<Consumer> ConsumerRepository { get; set; }
        public MethodResult Strategy(int connectionId)
        {
            try
            {

            }
            catch(Exception e)
            {

            }
            return null;
        }
    }
}