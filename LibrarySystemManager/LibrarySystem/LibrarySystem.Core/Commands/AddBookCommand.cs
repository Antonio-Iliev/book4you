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
<<<<<<< HEAD
        public string Execute(IEnumerable<string> parameters)
=======
        private readonly IBooksServices booksServices;

        public AddBookCommand(IBooksServices booksServices)
        {
            this.booksServices = booksServices;
        }

        public string Execute(IList<string> parameters)
>>>>>>> b60715f633c0c40ded833fa4201dc02cb3f3ad09
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
