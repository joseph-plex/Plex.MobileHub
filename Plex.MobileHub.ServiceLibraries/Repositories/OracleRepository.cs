using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using System.Reflection;
using Oracle.DataAccess.Client;

namespace Plex.MobileHub.ServiceLibraries.Repositories
{
    public class OracleRepository<T> : IRepository<T> where T : IRepositoryEntry, new()
    {

        public IList<String> PrimaryKeys
        {
            get
            {
                return primaryKeys.AsReadOnly();
            }
            protected set
            {
                primaryKeys = new List<String>(value);
            }
        }
        public IList<String> Properties { get; protected set; }

        public String InsertText { get; protected set; }
        public String UpdateText { get; protected set; }
        public String DeleteText { get; protected set; }
        public String SelectText { get; protected set; }

        List<String> primaryKeys;


        public OracleRepository()
        {
            PrimaryKeys = new List<String>(new T().GetPrimaryKeys());
            Properties = new List<String>(GetEntryPropertyNames(typeof(T))).AsReadOnly();
            InsertText = GenerateInsertText();
        }

        public void Insert(T Entry)
        {
            throw new NotImplementedException();
        }

        public void Update(Predicate<T> predicate, T Entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public T Retrieve(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all property names from a type.
        /// </summary>
        /// <returns>IEnumerable of strings</returns>
        static IEnumerable<String> GetEntryPropertyNames(Type type)
        {
            foreach (var propInfo in type.GetProperties())
                yield return propInfo.Name;
        }
        
        String GenerateInsertText()
        {
            String statement = "INSERT INTO {0}({1}) VALUES ({2})";
            //Do first entry here becasse its unique input.
            String columnNames = Properties.First();
            String placeHolders = ":a" + 0;

            //Start @ 1 since first entry is already done
            for (int i = 1; i < Properties.Count; i++)
            {
                columnNames += ", " + Properties[i];
                placeHolders += ", :a" + i;
            }

            statement = String.Format(statement, typeof(T).Name, columnNames, placeHolders);
            return statement;
        }

    }
}
