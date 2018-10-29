using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System.Linq;
using LibrarySystem.Services.ViewModels;
using System.Collections.Generic;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using LibrarySystem.Services.Exceptions.GenreServices;
using LibrarySystem.Services.Exceptions.AuthorServices;
using LibrarySystem.Services.Abstract.Contracts;
using System;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data.Context;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public BookViewModel AddBook(string title, int genreId, int authorId, string bookInStore)
        {
            this.validations.BookTitleValidation(title);

            int numberOfBook;

            try
            {
                numberOfBook = int.Parse(bookInStore);
            }
            catch (FormatException)
            {
                throw new InvalidBookServiceParametersExeption
                    ("This is not a number. You need a count of books");
            }

            if (numberOfBook < 1)
            {
                throw new InvalidBookServiceParametersExeption
                    ("The count of books cannot be negative number");
            }
            this.validations.BookInStoreValidation(numberOfBook);

            var currentBook = this.context.Books
                .FirstOrDefault(b => b.Title == title);

            if (currentBook == null)
            {
                currentBook = new Book
                {
                    Title = title,
                    GenreId = genreId,
                    AuthorId = authorId,
                    BooksInStore = numberOfBook
                };

                this.context.Books.Add(currentBook);
            }
            else
            {
                currentBook.BooksInStore += numberOfBook;
            }

            this.context.SaveChanges();

            var bookToReturn = new BookViewModel
            {
                Title = currentBook.Title,
            };

            return bookToReturn;
        }

        public BookViewModel GetBook(string bookTitle)
        {
            this.validations.BookTitleValidation(bookTitle);

            var findBook = this.context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Title == bookTitle);

            if (findBook == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library.");
            }

            var bookToReturn = new BookViewModel
                {
                    Title = findBook.Title,
                    Author = findBook.Author.Name,
                    Genre = findBook.Genre.GenreName
                };

            return bookToReturn;
        }

        public IEnumerable<BookViewModel> ListOfBooksByGenre(string byGenre)
        {
            this.validations.GenreValidation(byGenre);

            var booksByGenre = this.context.Books
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
            this.validations.AuthorValidation(byAuthor);

            var booksByAuthor = this.context.Books
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

        public IEnumerable<BookViewModel> ListBooks()
        {
            IEnumerable<BookViewModel> books = this.context.Books
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Author = b.Author.Name,
                    Genre = b.Genre.GenreName
                }).ToList();

            return books;
        }
    }
}
