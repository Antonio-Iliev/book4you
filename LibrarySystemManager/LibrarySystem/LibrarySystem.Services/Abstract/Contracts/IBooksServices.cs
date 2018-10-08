using LibrarySystem.Data.Models;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, int genreId, int authorId, int bookInStore);
        string GetBook(string bookTitel);
    }
}