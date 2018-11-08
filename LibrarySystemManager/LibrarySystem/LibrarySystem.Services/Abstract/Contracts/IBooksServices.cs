using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(Book newBook);

        Book GetBookById(Guid bookId);

        IEnumerable<Book> ListBooks();

        IEnumerable<Book> ListBooks(string searchBy, string parameters);

        Book RemoveBook(Guid id);

        IEnumerable<Book> ListBooks(string searchBy, string parameters, int pageSize, int page = 1);

    }
}