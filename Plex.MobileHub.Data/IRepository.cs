﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public interface IRepository<T> where T : IRepositoryEntry, new()
    {
        event RepoEventHandler InsertEvent;
        event RepoEventHandler UpdateEvent;
        event RepoEventHandler DeleteEvent;
        IList<String> PrimaryKeys { get; }
        
        void Insert(T Entry);
        void Update(T Entry);
        void Delete(Predicate<T> predicate);
        bool Exists(Predicate<T> predicate);
        T Retrieve(Predicate<T> predicate);

        IEnumerable<T> RetrieveAll();

    }
}
