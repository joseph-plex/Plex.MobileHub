using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.AccessService;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public interface IAccessService 
    {
        IList<String> GetPrimayKeys(String typeName);
        void Insert(String typeName, Object Entry);
        void Update(String typeName, Object Entry);
        void Delete(String typeName, Object [] Entry);
        IRepositoryEntry[] RetrieveAll(String TypeName);
    }
}
