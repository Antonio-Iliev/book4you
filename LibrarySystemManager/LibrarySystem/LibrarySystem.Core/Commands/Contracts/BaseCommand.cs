using LibrarySystem.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands.Contracts
{
    public abstract class BaseCommand : ICommand
    {
        protected ILibrarySystemContext LibraryContext { get; }

        protected BaseCommand(ILibrarySystemContext libraryContext)
        {
            LibraryContext = libraryContext;
        }
        public abstract string Execute(IEnumerable<string> parameters);
       
    }
}
