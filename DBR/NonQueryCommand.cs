using System;
using System.Collections.Generic;

namespace DBR
{
    public class NonQueryCommand : CommandBuilder
    {
        public NonQueryCommand(string db_name, string table_name) : base(db_name, table_name, false)
        { }

        public override List<object> ExecuteQuery()
        {
            return null;
        }

        public override void ExecuteNonQuery()
        {
            ProcessNonQuery(db_name, command);
        }

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
