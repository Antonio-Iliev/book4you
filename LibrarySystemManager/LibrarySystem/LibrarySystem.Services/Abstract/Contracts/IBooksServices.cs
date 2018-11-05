using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, Genre genre, Author author, string bookInStore);

        Book GetBookByTitle(string bookTitel);

        Book GetBookById(Guid bookId);

        IEnumerable<Book> ListOfBooksByGenre(string byGenre);

        IEnumerable<Book> ListOfBooksByAuthor(string byAuthor);

        IEnumerable<Book> ListBooks();
    }
}