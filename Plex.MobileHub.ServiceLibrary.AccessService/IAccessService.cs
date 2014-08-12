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
    [ServiceContract]
    public interface IAccessService 
    {
        [OperationContract]
        IList<String> GetPrimayKeys(String typeName);
        [OperationContract]
        void Insert(String typeName, __TypeBase Entry);
        [OperationContract]
        void Update(String typeName, __TypeBase Entry);
        [OperationContract]
        void Delete(String typeName, __TypeBase[] Entry);
        [OperationContract]
        __TypeBase[] RetrieveAll(String TypeName);
    }
}
