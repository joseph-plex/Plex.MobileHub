using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
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

        String insertText;
        String updateText;
        String deleteText;
        String selectText;

        List<String> primaryKeys;


        public OracleRepository()
        {
            PrimaryKeys = new List<String>(new T().PrimaryKeys);
            Properties = new List<String>(GetEntryPropertyNames(typeof(T))).AsReadOnly();
            insertText = GenerateInsertText();
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
            String statement = "INSERT INTO {0} VALUES ({1})";
            String placeHolders = Properties.First();
            String columnNames = ":a" + 0;

            for (int i = Properties.IndexOf(placeHolders); i < placeHolders.Length; i++)
            {
                placeHolders += ", " + Properties[i];
                columnNames += ", :a" + i;
            }

            statement = String.Format(statement, columnNames, placeHolders);
            return statement;
        }
    }
}
