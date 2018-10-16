using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    class ListBooksCommand : ICommand
    {
        private readonly IBooksServices booksServices;

        public ListBooksCommand(IBooksServices booksServices)
        {
            this.booksServices = booksServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            var books = this.booksServices.ListBooks();

            StringBuilder str = new StringBuilder();

            foreach (var book in books)
            {
                str.AppendLine($"Title '{book.Title}' by {book.Author} - {book.Genre}");
            }

            return str.ToString().Trim();
        }
    }
}
