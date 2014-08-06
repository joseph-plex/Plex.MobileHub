using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.ManagementService;
namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ManagementService.svc or ManagementService.svc.cs at the Solution Explorer and start debugging.
    public class ManagementService : IManagementService
    {
        public void DoWork()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
