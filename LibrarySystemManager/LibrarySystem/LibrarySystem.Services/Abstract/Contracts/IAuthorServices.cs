using LibrarySystem.Data.Models;
using LibrarySystem.Services.ViewModels;

namespace LibrarySystem.Services.Services
{
    public interface IAuthorServices
    {
        int AddAuthor(string authorName);

        AuthorViewModel GetAuthor(string authorName);
    }
}