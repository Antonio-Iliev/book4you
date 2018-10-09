using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System.Linq;
using LibrarySystem.Services.Constants;
using LibrarySystem.Services.ViewModels;
using System.Collections.Generic;
using LibrarySystem.Services.Exceptions.GenreServiceExeptions;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using LibrarySystem.Services.Exceptions.GenreServices;
using LibrarySystem.Services.Exceptions.AuthorServices;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Book AddBook(string title, Genre genre, Author author, string bookInStore)
        {

            if (title.Length > ServicesConstants.MaxBookTitleLength
                || title.Length < ServicesConstants.MinBookTitleLength)
            {
                throw new InvalidBookServiceParametersExeption($"The book title '{title}' is more then " +
                    $"{ServicesConstants.MaxBookTitleLength} or " +
                    $"less then {ServicesConstants.MinBookTitleLength} symbols.");
            }

            int numberOfBook;
            if (!int.TryParse(bookInStore, out numberOfBook))
            {
                throw new InvalidBookServiceParametersExeption("Invalid number");
            }

            var newBook = this.context.Books.FirstOrDefault(b => b.Title == title);

            if (newBook != null)
            {
                throw new AddBookNullableExeption("The book already exist.");
            }

            newBook = new Book
            {
                Title = title,
                GenreId = genre.Id,
                AuthorId = author.Id,
                BooksInStore = numberOfBook
            };

            this.context.Books.Add(newBook);
            this.context.SaveChanges();
            return newBook;
        }

        public BookViewModel GetBook(string bookTitle)
        {

            if (bookTitle.Length > ServicesConstants.MaxBookTitleLength
                || bookTitle.Length < ServicesConstants.MinBookTitleLength)
            {
                throw new InvalidGenreServiceParametersExeption(
                   $"This book title can't be long then {ServicesConstants.MaxBookTitleLength} " +
                   $"or less then {ServicesConstants.MinBookTitleLength}");
            }

            var findBook = context.Books
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Author = b.Author.Name,
                    Genre = b.Genre.GenreName
                })
                .Where(b => b.Title == bookTitle).ToList();

            if (!findBook.Any())
            {
                throw new AddBookNullableExeption("There is no such book in this Library.");
            }

            return findBook[0];
        }

        public IEnumerable<BookViewModel> ListOfBooksByGenre(string byGenre)
        {
            if (byGenre.Length > ServicesConstants.MaxGenreNameLength
                || byGenre.Length < ServicesConstants.MinGenreNameLength)
            {
                throw new InvalidGenreServiceParametersExeption(
                    $"Genre can't be long then {ServicesConstants.MaxGenreNameLength} " +
                    $"or less then {ServicesConstants.MinGenreNameLength}");
            }

            var booksByGenre = context.Books
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Author = b.Author.Name,
                    Genre = b.Genre.GenreName
                })
                .Where(g => g.Genre == byGenre).ToList();

            if (!booksByGenre.Any())
            {
                throw new AddGenreNullableExeption("There is no such genre in this Library.");
            }

            return booksByGenre;
        }

        public IEnumerable<BookViewModel> ListOfBooksByAuthor(string byAuthor)
        {
            if (byAuthor.Length > ServicesConstants.MaxAuthorNameLength
                || byAuthor.Length < ServicesConstants.MinAuthorNameLength)
            {
                throw new InvalidGenreServiceParametersExeption(
                    $"Author can't be long then {ServicesConstants.MaxAuthorNameLength} " +
                    $"or less then {ServicesConstants.MinAuthorNameLength}");
            }

            var booksByAuthor = context.Books
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Author = b.Author.Name,
                    Genre = b.Genre.GenreName
                })
                .Where(g => g.Author == byAuthor).ToList();

            if (!booksByAuthor.Any())
            {
                throw new AddAuthorNullableExeption("There is no such author in this Library.");
            }

            return booksByAuthor;
        }
    }
}
