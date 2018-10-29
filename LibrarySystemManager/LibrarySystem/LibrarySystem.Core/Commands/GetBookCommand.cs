using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
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
                throw new InvalidBookServiceParametersExeption("Invalid parameters");
            }

            string bookTitle = args[0];

            var book = this.booksServices.GetBook(bookTitle);
           
            return $"Title '{book.Title}' by {book.Author.Name} - {book.Genre.GenreName}";
        }
    }
}
