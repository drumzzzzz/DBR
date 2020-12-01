using System;
using System.Collections.Generic;

namespace DBR
{
    public class CommandContext
    {
        public List<object> Execute(CommandBuilder qb)
        {
            CommandBuilder.COMMAND_TYPE command_type = qb.GetCommandType();
            Console.WriteLine("\nExecuting {0} Command: {1}", command_type, qb.GetCommand());

            switch (command_type)
            {
                case CommandBuilder.COMMAND_TYPE.QUERY_RECORDS:
                    return qb.ExecuteQuery();
                case CommandBuilder.COMMAND_TYPE.NONQUERY:
                {
                    qb.ExecuteNonQuery();
                    return null;
                }
            }

            throw new Exception(string.Format("Invalid Command Type: {0}", command_type));
        }

        public int ExecuteCount(CommandBuilder qb)
        {
            CommandBuilder.COMMAND_TYPE command_type = qb.GetCommandType();
            Console.WriteLine("\nExecuting {0} Command: {1}", command_type, qb.GetCommand());

            return qb.ExecuteCountQuery();
        }
    }
}
