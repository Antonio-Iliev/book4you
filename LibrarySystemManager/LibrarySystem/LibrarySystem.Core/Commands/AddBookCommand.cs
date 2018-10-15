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
            IList<string> args = parameters.ToList();

            if (args.Count != 4)
            {
                throw new ArgumentException("Invalid numbers of parameters");
            }

            string title = args[0];
            string genre = args[1];
            string author = args[2];
            string bookInStore = args[3];

            var newGenre = genreServices.AddGenre(genre);
            var newAuthor = authorServices.AddAuthor(author);

            var addedBook = booksServices.AddBook(title, newGenre, newAuthor, bookInStore);

            return $"New book {addedBook.Title} was added.";
        }
    }
}
