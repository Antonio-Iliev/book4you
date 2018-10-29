using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IAuthorServices
    {
        int AddAuthor(string authorName);

        Author GetAuthor(string authorName);
    }
}