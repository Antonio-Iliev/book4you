using LibrarySystem.Data.Models;
using LibrarySystem.Services.ViewModels;

namespace LibrarySystem.Services.Services
{
    public interface IGenreServices
    {
        int AddGenre(string genreName);

        GenreViewModel GetGenre(string genreName);
    }
}