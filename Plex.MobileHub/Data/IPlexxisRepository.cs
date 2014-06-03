using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public interface IRepository <T> where T : IPlexxisDatabaseRow
    {
        IEnumerable<T> GetAll();
        void Insert();
        void Update();
        void Delete();
    }
}
