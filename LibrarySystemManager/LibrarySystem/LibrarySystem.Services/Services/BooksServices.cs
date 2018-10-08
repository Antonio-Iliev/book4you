using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Book AddBook(string title, int genreId, int authorId, int bookInStore)
        {
            var newBook = this.context.Books.FirstOrDefault(b => b.Title == title);

            if (newBook != null)
            {
                throw new ArgumentException("The book already exist.");
            }

            newBook = new Book
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
                BooksInStore = bookInStore
            };

            this.context.Books.Add(newBook);
            this.context.SaveChanges();
            return newBook;
        }

        public string GetBook(string bookTitle)
        {
            var query = context.Books
                .Select(b => new
                {
                    Title = b.Title,
                    Author = b.Author.Name,
                    Genre = b.Genre.GenreName
                }).Where(b => b.Title == bookTitle).ToList();

            if (!query.Any())
            {
                return "There is no such book in this the Library.";
            }

            var book = query[0];
            return $"{book.Title}, {book.Author}, {book.Genre}";
        }
    }
}
