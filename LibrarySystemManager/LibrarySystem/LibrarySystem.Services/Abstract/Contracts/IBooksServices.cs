using LibrarySystem.Data.Models;
using LibrarySystem.Services.ViewModels;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, Genre genre, Author author, string bookInStore);
        BookViewModel GetBook(string bookTitel);
    }
}