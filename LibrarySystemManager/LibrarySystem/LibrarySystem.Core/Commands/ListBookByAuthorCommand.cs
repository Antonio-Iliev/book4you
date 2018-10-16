using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class ListBookByAuthorCommand : ICommand
    {
        private readonly IBooksServices booksServices;

        public ListBookByAuthorCommand(IBooksServices booksServices)
        {
            this.booksServices = booksServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            IList<string> args = parameters.ToList();

            if (args.Count != 1)
            {
                throw new InvalidBookServiceParametersExeption("Invalid parameters");
            }

            string author = args[0];

            var listOfBooks = this.booksServices.ListOfBooksByAuthor(author);

            StringBuilder str = new StringBuilder();

            foreach (var book in listOfBooks)
            {
                str.AppendLine($"Title '{book.Title}' by {book.Author} - {book.Genre}");
            }

            return str.ToString().Trim();
        }
    }
}
