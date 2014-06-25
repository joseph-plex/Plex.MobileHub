using System;
using System.Linq;
using System.Collections.Generic;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data;
//using Plex.MobileHub.ServiceLibraries.
namespace Plex.MobileHub.ServiceLibraries.Test 
{
    public class InMemoryRepository<T> : IRepository<T> where T : RepositoryEntryBase, IRepositoryEntry, new()
    {
        public event RepoEventHandler InsertEvent;
        public event RepoEventHandler UpdateEvent;
        public event RepoEventHandler DeleteEvent;

        public IList<String> PrimaryKeys { get; protected set; }
        List<T> data;
        public InMemoryRepository() {
            PrimaryKeys = new List<String>(new T().GetPrimaryKeys());
            data = new List<T>();
        }

        public void  Insert(T Entry){
            if(Get(Entry) != null)
                throw new Exception("Duplicate Entry - Identical Key already exists");
            data.Add(Entry);
            if (InsertEvent != null)
                InsertEvent(this, new RepositoryOperationEventArgs() { Entry = Entry });
        }
        
        public void Update(T Entry){
            var obj = Get(Entry);
            if (obj == null)
                throw new Exception("Object does not exist");
            obj = Entry;
            if (UpdateEvent != null)
                UpdateEvent(this, new RepositoryOperationEventArgs() { Entry = obj });
        }

        public void Delete(Predicate<T> predicate)
        {
            data.RemoveAll(predicate);
            if (DeleteEvent != null)
                DeleteEvent(this, new RepositoryOperationEventArgs() { Entry = null });
        }

        public bool Exists(Predicate<T> predicate)
        {
            return data.Exists(predicate);
        }

        public T Retrieve(Predicate<T> predicate)
        {
            int index = data.FindIndex(predicate);
            return (index != -1)? data[index] : null;
        }

        public IEnumerable<T> RetrieveAll()
        {
            return data.ToArray();
        }

        T Get(T Entry)
        {
            //Returns Entry based on Identical PrimaryKeys
            Type entryType = typeof(T);
            var KeyPropertyInfo = entryType.GetProperties().Where(p => PrimaryKeys.Any(p2 => p2 == p.Name));

            foreach (var v in data)
            {
                //Assume the objects are identical by default to prevent false positives.
                Boolean AlreadyExists = true;
                foreach (var property in KeyPropertyInfo)
                    if (property.GetValue(v) != property.GetValue(Entry))
                        AlreadyExists = false;
                if (AlreadyExists)
                    return v;
            }
            return null;
        }
    }
}
