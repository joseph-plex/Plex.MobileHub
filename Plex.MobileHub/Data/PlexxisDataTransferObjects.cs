using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using Plex.MobileHub.Repositories;
using System.Collections;
namespace Plex.MobileHub.Data
{
    public abstract class PlexxisDataTransferObjects : IPlexxisDatabaseRow
    {

        #region Properties
        protected virtual List<String> PrimaryKey
        {
            get;
            set;
        }
        protected virtual String InsertText
        {
            get
            {
                if (String.Empty == (insertText ?? String.Empty))
                    insertText = GenerateInsertText();
                return insertText;
            }
            set
            {
                insertText = value;
            }
        }
        protected virtual String UpdateText
        {
            get
            {
                if (String.Empty == (updateText ?? String.Empty))
                    updateText = GenerateInsertText();
                return updateText;
            }
            set
            {
                updateText = value;
            }
        }
        protected virtual String DeleteText
        {
            get
            {
                if (String.Empty == (deleteText ?? String.Empty))
                    deleteText = GenerateInsertText();
                return deleteText;
            }
            set
            {
                deleteText = value;
            }
        }
        #endregion

        #region Fields
        string insertText;
        string updateText;
        string deleteText;
        #endregion

        #region Constructors
        //Constructors
        public PlexxisDataTransferObjects()
        {
            PrimaryKey = new List<String>();
            insertText = String.Empty;
            updateText = String.Empty;
            deleteText = String.Empty;
        }

        #endregion

        #region Public Methods
        public String GetInsertText()
        {
            return InsertText;
        }

        public String GetUpdateText()
        {
            return UpdateText;
        }

        public String GetDeleteText()
        {
            return DeleteText;
        }

        public void Randomize() 
        {

        }
        #endregion

        #region Public Virtual Methods
        //Methods
        public virtual bool Insert(IDbConnection Conn)
        {
            string sql1 = "INSERT INTO ^T^";
            string sql2 = " ^I^ ";
            string sql3 = "VALUES ^V^";

            string sql = string.Empty;

            var fields = GetType().GetProperties();
            string TableName = this.GetType().Name;

            string ret = "(*)";
            string FormattedIdentifers = string.Empty;

            if (fields.Length > 0)
            {
                var It = fields.GetEnumerator();
                bool first = true;
                while (It.MoveNext())
                {
                    if (first)
                    {
                        FormattedIdentifers += ((FieldInfo)It.Current).Name;
                        first = false;
                    }
                    else
                        FormattedIdentifers += ", " + ((FieldInfo)It.Current).Name;
                }
            }

            string Idenitfers = ret.Replace("*", FormattedIdentifers);

            string BindingStart = ":a";
            string ValueBindings = "(*)";
            string ValBindingsInner = string.Empty;

            if (fields.Length <= 0) //This cannot be possible, becauuse it would mean there are no columns in the table (in theory)
                throw new Exception();

            var Iterator = fields.GetEnumerator();
            for (int i = 0; Iterator.MoveNext(); i++)
                if (i == 0) ValBindingsInner += BindingStart + i;
                else ValBindingsInner += ", " + BindingStart + i;
            ValueBindings = ValueBindings.Replace("*", ValBindingsInner);

            sql = sql1.Replace("^T^", TableName) + sql2.Replace("^I^", Idenitfers) + sql3.Replace("^V^", ValueBindings);


            using (var Command = Conn.CreateCommand())
            {
                Command.CommandText = sql;
                for (int i = 0; i < fields.Length; i++)
                {
                    var param = Command.CreateParameter();
                    param.Value = fields[i].GetValue(this);
                    Command.Parameters.Add(param);
                }
                return Convert.ToBoolean(Command.ExecuteNonQuery());
            }
        }

        public virtual bool Update(IDbConnection Conn)
        {
            var FieldNames = new List<string>();
            var fields = GetType().GetProperties().ToList();
            fields.ForEach((x) => FieldNames.Add(x.Name));
            FieldNames = FieldNames.FindAll((p) => !PrimaryKey.Contains(p));

            if (FieldNames.Count == 0)
                throw new NotImplementedException("Updates are not supported because you cannot update any information in this table.");


            using (var Command = Conn.CreateCommand(UpdateText))
            {
                for (int i = 0; i < FieldNames.Count; i++)
                    Command.Parameters.Add(Command.CreateParameter(fields.Find((p) => FieldNames[i] == p.Name).GetValue(this)));

                for (int i = 0; i < (fields.Count - FieldNames.Count); i++)
                    Command.Parameters.Add(Command.CreateParameter(fields.Find((p) => PrimaryKey[i] == p.Name).GetValue(this)));

                return Convert.ToBoolean(Command.ExecuteNonQuery());
            }
        }

        public virtual bool Delete(IDbConnection Conn)
        {
            string sql1 = "DELETE FROM ^T^ ";
            string sql2 = "WHERE ^C^ ";

            string sql = string.Empty;
            string BindingStart = ":a";

            string condition = string.Empty;
            string TableName = GetType().Name;

            var fields = GetType().GetProperties().ToList();

            for (int i = 0; i < PrimaryKey.Count; i++)
                condition += ((i != 0) ? "," : "") + PrimaryKey[i] + "=" + BindingStart + i;

            sql = sql1.Replace("^T^", TableName) + sql2.Replace("^C^", condition);
            using (var Command = Conn.CreateCommand(sql))
            {
                for (int i = 0; i < PrimaryKey.Count; i++)
                    Command.CreateParameter(fields.Find((p) => PrimaryKey[i] == p.Name).GetValue(this));
                return Convert.ToBoolean(Command.ExecuteNonQuery());
            }
        }
        

        public virtual bool Insert()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Insert(Conn);
        }

        public virtual bool Update()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Update(Conn);
        }

        public virtual bool Delete()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Delete(Conn);
        }
        #endregion

        #region Protected Methods
        protected void AutoFill(IDataReader reader, Object obj)
        {
            foreach (var f in obj.GetType().GetProperties())
                f.SetValue(obj, (reader[f.Name] != DBNull.Value) ? (Convert.ChangeType(reader[f.Name], Nullable.GetUnderlyingType(f.PropertyType) ?? f.PropertyType)) : null);
        }
        #endregion

        #region Private Methods
        string GenerateInsertText()
        {
            //Init
            IEnumerator Iterator;
            Type type = GetType();
            var fields = type.GetProperties();
            String ValueBindings = String.Empty;
            String FormattedIdentifers = String.Empty;

            //Validation
            if (fields.Length <= 0)
                throw new NotSupportedException("Object not intended to express a table without columns ");

            //Get Column Identifiers
            Iterator = fields.GetEnumerator();
            for (bool first = true; Iterator.MoveNext(); first = false)
                FormattedIdentifers += ((first) ? String.Empty : ", ") + ((PropertyInfo)Iterator.Current).Name;
            FormattedIdentifers = "(" + FormattedIdentifers + ")";

            //Get Column Bindings
            Iterator = fields.GetEnumerator();
            for (int i = 0; Iterator.MoveNext(); i++)
                ValueBindings += ((i == 0) ? String.Empty : ", ") + ":a" + i;
            ValueBindings = " VALUES" + "(" + ValueBindings + ")";

            //return Insert Text
            return "INSERT INTO " + type.Name + FormattedIdentifers + ValueBindings;
        }
        string GenerateUpdateText()
        {
            string sql1 = "UPDATE ^T^ ";
            string sql2 = "SET ^CVP^ "; //Column Value Pairing
            string sql3 = "WHERE ^C^ ";

            string sql = string.Empty;
            string BindingStart = ":a";
            string condition = string.Empty;
            string TableName = GetType().Name;

            for (int i = 0; i < PrimaryKey.Count; i++)
                condition += ((i != 0) ? "," : "") + PrimaryKey[i] + "=" + BindingStart + i;

            var FieldNames = new List<string>();
            var fields = GetType().GetProperties().ToList();
            fields.ForEach((x) => FieldNames.Add(x.Name));
            FieldNames = FieldNames.FindAll((p) => !PrimaryKey.Contains(p));

            if (FieldNames.Count == 0)
                throw new NotImplementedException("Updates are not supported because you cannot update any information in this table.");

            string ColumnValuePairings = string.Empty;
            int varCount = PrimaryKey.Count + FieldNames.Count;
            for (int i = PrimaryKey.Count; i < varCount; i++)
                ColumnValuePairings += ((i != PrimaryKey.Count) ? "," : "") + FieldNames[i - PrimaryKey.Count] + " = " + BindingStart + i;

            sql = sql1.Replace("^T^", TableName) + sql2.Replace("^CVP^", ColumnValuePairings) + sql3.Replace("^C^", condition);
            return sql;
        }
        string GenerateDeleteText()
        {
            string sql1 = "DELETE FROM ^T^ ";
            string sql2 = "WHERE ^C^ ";

            string sql = string.Empty;
            string BindingStart = ":a";

            string condition = string.Empty;
            string TableName = GetType().Name;

            var fields = GetType().GetProperties().ToList();

            for (int i = 0; i < PrimaryKey.Count; i++)
                condition += ((i != 0) ? "," : "") + PrimaryKey[i] + "=" + BindingStart + i;

            sql = sql1.Replace("^T^", TableName) + sql2.Replace("^C^", condition);
            return sql;
        }

        #endregion
    }
}