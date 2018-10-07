using LibrarySystem.Data.Models;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        Book AddBook(string title, string genre, string author);
    }
}