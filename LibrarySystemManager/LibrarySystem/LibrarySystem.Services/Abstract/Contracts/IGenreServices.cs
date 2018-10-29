using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IGenreServices
    {
        int AddGenre(string genreName);

        Genre GetGenre(string genreName);
    }
}