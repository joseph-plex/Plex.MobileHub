using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime;
namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public class Delete : MethodStrategyBase<Object>
    {
        public static T Cast<T>(object o)
        {
            return (T)o;
        }
        public static Predicate<T> CreatePredicate<T>()
        {
            return new Predicate<T>(p => true);
        }

        public static Boolean Exists<T>(T Entry, IRepository<T> data)
            where T : IRepositoryEntry, new()
        {
            //Returns Entry based on Identical PrimaryKeys
            Type entryType = typeof(T);
            var KeyPropertyInfo = entryType.GetProperties().
                Where(p => Entry.GetPrimaryKeys().
                    Any(p2 => p2 == p.Name));

            foreach (var v in data.RetrieveAll())
            {
                //Assume the objects are identical by default to prevent false positives.
                Boolean AlreadyExists = true;
                foreach (var property in KeyPropertyInfo)
                    if (!property.GetValue(v).Equals(property.GetValue(Entry)))
                        AlreadyExists = false;
                if (AlreadyExists)
                    return true;
            }
            return false;

        }
        public void Strategy(string typeName, object[] entry)
        {
            //Call a type Repository. 
            var type = Repositories.Keys.First(p => p.FullName == typeName);
            var RepoType = Repositories[type].GetType();
            var castMethod = GetType().GetMethod("Cast");
            var delete = RepoType.GetMethod("Delete", new Type[]{ GetType().GetMethod("CreatePredicate").MakeGenericMethod(type).Invoke(null,null).GetType() });

            var exist = GetType().GetMethod("Exists").MakeGenericMethod(type);
            Func<IRepositoryEntry, Boolean> func = (t) => (Boolean)exist.Invoke(null, new object[]{ 
                castMethod.MakeGenericMethod(type).Invoke(null, new Object[] { t }),
                Repositories[type]
            });
            delete.Invoke(Repositories[type], new Object[] { new Predicate<IRepositoryEntry>(func) });
        }

         public void AltStrategy(string typeName, object[] entry)
        {
            //Call a type Repository. 
            var type = Repositories.Keys.First(p => p.FullName == typeName);
            var RepoType = Repositories[type].GetType();
            var castMethod = GetType().GetMethod("Cast");
            var pred = GetType().GetMethod("CreatePredicate").MakeGenericMethod(type);

            
            var deleteMethods = RepoType.GetMethods().Where(p => p.GetParameters().Length == 1 && p.Name == "Delete");

            if (deleteMethods.Count() != 1)
                throw new NotSupportedException();

            var delete = deleteMethods.First();

            var exist = GetType().GetMethod("Exists").MakeGenericMethod(type);
            Func<IRepositoryEntry, Boolean> func = (t) => (Boolean)exist.Invoke(null, new object[]{ 
                castMethod.MakeGenericMethod(type).Invoke(null, new Object[] { t }),
                Repositories[type]
            });
            delete.Invoke(Repositories[type], new Object[] { new Predicate<IRepositoryEntry>(func) });
        }
    }
}
