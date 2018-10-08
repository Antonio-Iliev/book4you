using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }

            if (args[0].Length > CommandConstants.MaxBookTitleLength)
            {
                return CommandConstants.BookDoesNotExist;
            }

            var thisBook = this.booksServices.GetBook(args[0]);

            return $"Book Title: {thisBook.Title}, " +
                $"Author: {thisBook.Author.Name}, " +
                $"Genre: {thisBook.Genre.GenreName} " +
                $"Available books {thisBook.BooksInStore}";
        }
    }
}
