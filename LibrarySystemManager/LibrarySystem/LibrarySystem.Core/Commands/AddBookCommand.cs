using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Context;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IBooksServices booksServices;

        public AddBookCommand(IBooksServices booksServices)
        {
            this.booksServices = booksServices;
        }

        public string Execute(IList<string> parameters)
        {
            parameters = parameters.Where(p => p != null && p != "").ToList();

            if (parameters.Count != 3)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }

            booksServices.AddBook(parameters[0], parameters[1], parameters[2]);

            //TODO add Title
            return $"New book TITLE was added.";
        }
    }
}
