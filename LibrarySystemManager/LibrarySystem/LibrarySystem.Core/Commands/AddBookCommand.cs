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

        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.Where(p => p != null && p != "").ToList();

            if (args.Count != 3)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }

            string title = args[0];
            string genre = args[1];
            string author = args[2];

            if (title.Length > CommandConstants.MaxBookTitleLength)
            {
                return $"The book title '{title}' is more then " +
                    $"{CommandConstants.MaxBookTitleLength} symbols.";
            }
            if (genre.Length > CommandConstants.MaxGenreNameLength)
            {
                return $"The book genre '{genre}' is more then " +
                    $"{CommandConstants.MaxGenreNameLength} symbols.";
            }
            if (author.Length > CommandConstants.MaxAuthorNameLength)
            {
                return $"The book author name '{author}' is more then " +
                    $"{CommandConstants.MaxAuthorNameLength} symbols.";
            }

            booksServices.AddBook(title, genre, author);

            return $"New book {title} was added.";
        }
    }
}
