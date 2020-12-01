using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using FastMember;

namespace DBR
{
    public abstract class Database
    {
        // Base DB connection string
        private const string DB_PATH = "URI=file:";

        #region Type Accessor Caching
        // Caches any new object type accessor to dictionary
        // Returns instance on repeated request
        private static readonly Dictionary<Type, TypeAccessor> dTypeAccessors = new Dictionary<Type, TypeAccessor>();

        private static TypeAccessor GetTypeAccessor(Type type)
        {
            if (!dTypeAccessors.ContainsKey(type))
            {
                dTypeAccessors.Add(type, TypeAccessor.Create(type, false));
            }
            return dTypeAccessors[type];
        }

        #endregion

        #region Methods

        // Returns a reflected object list results of a supplied type from a given query command
        protected static List<object> ProcessQuery(string db, Type type, string command)
        {
            List<object> objList = new List<object>();
            SQLiteConnection conn = new SQLiteConnection(DB_PATH + db + ";");

            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn)
                {
                    CommandText = command
                };

                SQLiteDataReader rdr = cmd.ExecuteReader();
                TypeAccessor ta_dest = GetTypeAccessor(type);
                MemberSet ta_members = ta_dest.GetMembers();
                string fieldname;

                while (rdr.Read())
                {
                    object instance = Activator.CreateInstance(type);

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        fieldname = rdr.GetName(i);
                        if (!rdr.IsDBNull(i) && ta_members.Any(m => string.Equals(m.Name, fieldname, StringComparison.OrdinalIgnoreCase)))
                        {
                            var fvalue = rdr.GetValue(i);
                            ta_dest[instance, fieldname] = fvalue;
                        }
                    }
                    objList.Add(instance);
                }
                rdr.Close();
                CloseConnection(conn);
                return objList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        // Performs a supplied non query command 
        protected static void ProcessNonQuery(string db, string command)
        {
            SQLiteConnection conn = new SQLiteConnection(DB_PATH + db + ";");

            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn)
                {
                    CommandText = command
                };

                cmd.ExecuteNonQuery();
                CloseConnection(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        // Returns a row count from a given command 
        protected static int ProcessCountQuery(string db, string command)
        {
            SQLiteConnection conn = new SQLiteConnection(DB_PATH + db + ";");

            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn)
                {
                    CommandText = command
                };

                int RowCount = Convert.ToInt32(cmd.ExecuteScalar());
                CloseConnection(conn);
                return RowCount;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Connection

        protected static void CloseConnection(SQLiteConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Cancel();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}
