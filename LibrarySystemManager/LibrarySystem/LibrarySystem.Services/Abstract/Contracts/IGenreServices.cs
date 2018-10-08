using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IGenreServices
    {
        Genre AddGenre(string genreName);
    }
}