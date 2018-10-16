using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
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
                string commandName;
                IList<string> commandParams = new List<string>();
                if (!commandLine.Contains(' '))
                {
                    commandName = commandLine.ToLower();
                }
                else
                {
                    commandName = commandLine.Substring(0, commandLine.IndexOf(' ')).ToLower();

                    commandParams = commandLine
                        .Substring(commandName.Length)
                        .Split(",")
                        .Select(c => c.Trim())
                        .ToList();
                }

                ICommand command;
                try
                {
                    command = this.autofacContext.ResolveNamed<ICommand>(commandName);
                }
                catch (DependencyResolutionException)
                {
                    throw new Exception("There is not such command.");
                }

                results.Add(command.Execute(commandParams));
            }
            return results;
        }
    }
}
