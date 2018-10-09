using LibrarySystem.Data.Models;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, Genre genre, Author author, string bookInStore);
        void GetBook(string bookTitel);
    }
}