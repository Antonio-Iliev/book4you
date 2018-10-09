using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class GetBookCommand : ICommand
    {
        private readonly IBooksServices booksServices;

        public GetBookCommand(IBooksServices booksServices)
        {
            this.booksServices = booksServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            IList<string> args = parameters.ToList();

            if (args.Count != 1)
            {
                throw new ArgumentException("Invalid parameters");
            }
            this.booksServices.GetBook(args[0]);

            return "";
        }
    }
}
