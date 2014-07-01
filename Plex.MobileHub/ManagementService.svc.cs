using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibraries.ManagementServiceLibrary;

namespace Plex.MobileHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Management" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Management.svc or Management.svc.cs at the Solution Explorer and start debugging.
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
