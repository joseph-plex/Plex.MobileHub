using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public interface IRepository<T> where T : IRepositoryEntry, new()
    {
        void Insert(T Entry);
        void Update(Predicate<T> predicate, T Entry);
        void Delete(Predicate<T> predicate);
        T Retrieve(Predicate<T> predicate);
        IEnumerable<T> RetrieveAll();
    }
}
