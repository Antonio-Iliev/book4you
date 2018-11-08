using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, Genre genre, Author author, string bookInStore);

        Book GetBookById(Guid bookId);

        IEnumerable<Book> ListBooks();

<<<<<<< HEAD
        IEnumerable<Book> ListBooks(string searchBy, string parameters);

        Book RemoveBook(Guid id);
=======
        IEnumerable<Book> ListBooks(string searchBy, string parameters, int pageSize, int page = 1);
>>>>>>> 9ca6ad411e784a75ff0d3c6460e48411c662dfc9
    }
}