using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IAuthorServices
    {
        Author AddAuthor(string authorName);
    }
}