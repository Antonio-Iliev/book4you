using LibrarySystem.Data.Models;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, Genre genre, Author author, string bookInStore);

        Book GetBook(string bookTitel);

        IEnumerable<Book> ListOfBooksByGenre(string byGenre);

        IEnumerable<Book> ListOfBooksByAuthor(string byAuthor);

        IEnumerable<Book> ListBooks();
    }
}