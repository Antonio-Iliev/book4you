using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System.Linq;
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

        public Book AddBook(string title, Genre genre, Author author, string bookInStore)
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

            var book = this.context.Books
                .FirstOrDefault(b => b.Title == title);

            if (book == null)
            {
                book = new Book
                {
                    Title = title,
                    GenreId = genre.Id,
                    AuthorId = author.Id,
                    BooksInStore = numberOfBook
                };

                this.context.Books.Add(book);
            }
            else
            {
                book.BooksInStore += numberOfBook;
            }

            this.context.SaveChanges();

            book.Author = author;
            book.Genre = genre;

            return book;
        }

        public Book GetBook(string bookTitle)
        {
            this.validations.BookTitleValidation(bookTitle);

            var book = this.context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Title == bookTitle);

            if (book == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library.");
            }

            return book;
        }

        public IEnumerable<Book> ListOfBooksByGenre(string byGenre)
        {
            this.validations.GenreValidation(byGenre);

            var booksByGenre = this.context.Books
                .Include(b => b.Author)
                .Include(g => g.Genre)
                .Where(b => b.Genre.GenreName == byGenre).ToList();

            if (!booksByGenre.Any())
            {
                throw new AddGenreNullableExeption("There is no such genre in this Library.");
            }

            return booksByGenre;
        }

        public IEnumerable<Book> ListOfBooksByAuthor(string byAuthor)
        {
            this.validations.AuthorValidation(byAuthor);

            var booksByAuthor = this.context.Books
                .Include(b => b.Author)
                .Include(g => g.Genre)
                .Where(a => a.Author.Name == byAuthor).ToList();

            if (!booksByAuthor.Any())
            {
                throw new AddAuthorNullableExeption("There is no such author in this Library.");
            }

            return booksByAuthor;
        }

        public IEnumerable<Book> ListBooks()
        {
            IEnumerable<Book> books = this.context.Books
                .Include(b => b.Author)
                .Include(g => g.Genre)
                .ToList();

            return books;
        }
    }
}
