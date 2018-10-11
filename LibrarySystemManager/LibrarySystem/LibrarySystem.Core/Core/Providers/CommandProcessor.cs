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
                var splittedCommand = commandLine.Split(",").Select(c => c.Trim()).ToList();
                var commandName = splittedCommand[0].ToLower();
                var commandParams = splittedCommand.Skip(1).ToList();
                var command = this.autofacContext.ResolveNamed<ICommand>(commandName);

                results.Add(command.Execute(commandParams));
            }
            return results;
        }
    }
}
