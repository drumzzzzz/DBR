using System;
using System.Collections.Generic;

namespace DBR
{
    public class QueryCountCommand : CommandBuilder
    {
        public QueryCountCommand(string db_name, string table_name) : base(db_name, table_name, true)
        { }

        public override List<object> ExecuteQuery()
        {
            return null;
        }

        public override void ExecuteNonQuery()
        { }

        public override int ExecuteCountQuery()
        {
            return ProcessCountQuery(db_name, command);
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
