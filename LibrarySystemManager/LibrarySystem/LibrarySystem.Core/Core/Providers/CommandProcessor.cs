using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.ConsoleClient.Core.Contracts;

namespace LibrarySystem.ConsoleClient.Core.Providers
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IComponentContext autofacContext;

        public CommandProcessor(IComponentContext autofacContext)
        {
            this.autofacContext = autofacContext;
        }

        public IEnumerable<string> ProcessCommands(IEnumerable<string> commands)
        {
            var results = new List<string>();
            foreach (string commandLine in commands)
            {
                string commandName = commandLine.Substring(0, commandLine.IndexOf(' ')).ToLower();

                var commandParams = commandLine
                    .Substring(commandName.Length)
                    .Split(",")
                    .Select(c => c.Trim())
                    .ToList();

                var command = this.autofacContext.ResolveNamed<ICommand>(commandName);

                results.Add(command.Execute(commandParams));
            }
            return results;
        }
    }
}
