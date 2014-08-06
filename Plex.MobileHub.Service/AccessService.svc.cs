using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.AccessService;

namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccessService : IAccessService
    {
        public void GetPrimayKeys(string typeName)
        {
            throw new NotImplementedException();
        }

        public void Insert(string typeName, object Entry)
        {
            throw new NotImplementedException();
        }

        public void Update(string typeName, object Entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(string typeName, object[] Entry)
        {
            throw new NotImplementedException();
        }

        public object[] RetrieveAll(string TypeName)
        {
            throw new NotImplementedException();
        }
    }
}
