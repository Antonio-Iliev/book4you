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
            var newBook = new Book
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
                BookInStore = bookInStore
            };

            this.context.Books.Add(newBook);
            this.context.SaveChanges();

            return newBook;
        }

        public Book GetBook(string bookName)
        {
            Book theBook = context.Books.SingleOrDefault(b => b.Title == bookName);

            return theBook;
        }
    }
}
