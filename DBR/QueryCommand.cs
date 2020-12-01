using System;
using System.Collections.Generic;

namespace DBR
{
    public class QueryCommand : CommandBuilder
    {
        public QueryCommand(string db_name, string table_name, Type object_type) : base(db_name, table_name, object_type)
        { }

        public override List<object> ExecuteQuery()
        {
            return ProcessQuery(db_name, object_type, command);
        }

        public override void ExecuteNonQuery()
        {}

        public override int ExecuteCountQuery()
        {
            return -1;
        }

        public override void SetCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new Exception("Invalid Command");

            this.command = command;
        }

        public override string GetCommand()
        {
            return string.IsNullOrEmpty(command) ? string.Empty : command;
        }

        public override COMMAND_TYPE GetCommandType()
        {
            return command_type;
        }
    }
}
