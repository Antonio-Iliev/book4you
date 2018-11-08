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

        public Book AddBook(Book newBook)
        {
            var book = this.context.Books
                .SingleOrDefault(b =>
                b.Title == newBook.Title 
                && b.AuthorId == newBook.AuthorId);

            if (book == null)
            {
                this.context.Books.Add(newBook);
                this.context.SaveChanges();
            }
            else
            {
                // TODO if any books left, should return number to user
                int newQuantity = book.BooksInStore + newBook.BooksInStore;
                book.BooksInStore = newQuantity == 20 ? 20 : newQuantity;
            }

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
