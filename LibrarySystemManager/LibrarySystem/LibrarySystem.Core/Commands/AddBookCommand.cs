using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IBooksServices booksServices;
        private readonly IGenreServices genreServices;
        private readonly IAuthorServices authorServices;

        public AddBookCommand(IBooksServices booksServices,
            IGenreServices genreServices,
            IAuthorServices authorServices)
        {
            this.booksServices = booksServices;
            this.genreServices = genreServices;
            this.authorServices = authorServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            // TODO remove Where
            IList<string> args = parameters.Where(p => p != null && p != "").ToList();

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

            Genre newGenre = genreServices.AddGenre(genre);
            Author newAuthor = authorServices.AddAuthor(author);

            booksServices.AddBook(title, newGenre.Id, newAuthor.Id, CommandConstants.InitialBookAmount);

            return $"New book {title} was added.";
        }
    }
}
