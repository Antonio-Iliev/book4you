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

            string title = parameters[0];
            string genre = parameters[1];
            string author = parameters[2];

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
