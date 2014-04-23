using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Objects;
using MobileHub.Repositories;
namespace MobileHub.Functionality.Clients
{
    public class GetCommands  : FunctionStrategyBase<List<Command>>
    {
        public List<Command> Strategy(int connectionId)
        {
            //Make sure that the connection exists
            if (!Connections.Instance.ConnectionExists(connectionId))
                throw new Exception("Invalid Connection Id");

            //Get information from connection
            var ConnectionDetails = Connections.Instance.Retrieve(connectionId);

            //Get all commands that are intended for that clientId
            return Commands.Instance.CommandRepo.Values.ToList().FindAll(p => p.ClientId == ConnectionDetails.ClientId);
        }
    }
}