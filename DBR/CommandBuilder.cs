using System;
using System.Collections.Generic;

namespace DBR
{
    public abstract class CommandBuilder : Database
    {
        protected string table_name;
        protected string db_name;
        protected Type object_type;
        protected string command;
        protected COMMAND_TYPE command_type;

        protected CommandBuilder(string db_name, string table_name, Type object_type)
        {
            if (string.IsNullOrEmpty(db_name))
                throw new Exception("Invalid Db Name");

            if (string.IsNullOrEmpty(table_name))
                throw new Exception("Invalid Table Name");

            if (object_type == null)
                throw new Exception("Invalid Object Type");

            this.db_name = db_name;
            this.table_name = table_name;
            this.object_type = object_type;

            command_type = COMMAND_TYPE.QUERY_RECORDS;
        }

        protected CommandBuilder(string db_name, string table_name, bool isCount)
        {
            if (string.IsNullOrEmpty(db_name))
                throw new Exception("Invalid Db Name");

            if (string.IsNullOrEmpty(table_name))
                throw new Exception("Invalid Table Name");

            this.db_name = db_name;
            this.table_name = table_name;

            command_type = (isCount) ? COMMAND_TYPE.QUERY_COUNT : COMMAND_TYPE.NONQUERY;
        }

        public enum COMMAND_TYPE
        {
            QUERY_RECORDS,
            QUERY_COUNT,
            NONQUERY
        }

        public abstract List<object> ExecuteQuery();
        public abstract void ExecuteNonQuery();
        public abstract int ExecuteCountQuery();
        public abstract void SetCommand(string command);
        public abstract string GetCommand();
        public abstract COMMAND_TYPE GetCommandType();
    }
}
