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
using LibrarySystem.Services.Constants.Enumeration;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(LibrarySystemContext context, IValidations validations)
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

        public Book GetBookById(Guid bookId)
        {
            var book = this.context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library.");
            }

            return book;
        }

        // (int page = 1) for paging
        public IEnumerable<Book> ListBooks()
        {
            IEnumerable<Book> books = this.context.Books
                .Include(b => b.Author)
                .Include(g => g.Genre)
                .ToList();

            return books;
        }

        public IEnumerable<Book> ListBooks
            (string searchBy, string parameters, int pageSize, int page)
        {
            if (!Enum.TryParse(searchBy.ToLower(), out SearchCategory key))
            {
                throw new InvalidBookServiceParametersExeption("Invalid input parameters");
            }

            IQueryable<Book> books = this.context.Books
                .Include(b => b.Author)
                .Include(g => g.Genre);

            // TODO is there better way?
            switch (key)
            {
                case SearchCategory.title:
                    books = books.Where(b => b.Title.Contains(parameters));
                    break;
                case SearchCategory.author:
                    books = books.Where(a => a.Author.Name.Contains(parameters));
                    break;
                case SearchCategory.genre:
                    books = books.Where(g => g.Genre.GenreName.Contains(parameters));
                    break;
            }

            int pageCount = (int)Math.Ceiling((double)books.Count() / pageSize);

            books = books
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return books;
        }

        public IEnumerable<Book> ListBooks(string searchBy, string parameters)
        {
            throw new NotImplementedException();
        }

        public Book RemoveBook(Guid id)
        {
            var book = GetBookById(id);
            book.BooksInStore = 0;
            this.context.SaveChanges();

            return book;
        }
    }
}
