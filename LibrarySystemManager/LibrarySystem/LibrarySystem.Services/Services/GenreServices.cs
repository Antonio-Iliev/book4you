using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.GenreServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class GenreServices : BaseServicesClass, IGenreServices
    {
        public GenreServices(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public int AddGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            Genre newGenre = this.context.Genres.FirstOrDefault(g => g.GenreName == genreName);

            if (newGenre == null)
            {
                this.context.Genres.Add(new Genre { GenreName = genreName });
                this.context.SaveChanges();
                newGenre = this.context.Genres.FirstOrDefault(g => g.GenreName == genreName);
            }

            return newGenre.Id;
        }

        public Genre GetGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            var genre = this.context.Genres
                .Include(b => b.Books)
                .FirstOrDefault(g => g.GenreName == genreName);

            if (genre == null)
            {
                throw new AddGenreNullableExeption("There is no such genre in this Library.");
            }

            return genre;
        }
    }
}
