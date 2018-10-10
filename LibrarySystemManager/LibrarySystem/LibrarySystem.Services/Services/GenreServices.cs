using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Services.Services
{
    public class GenreServices : BaseServicesClass, IGenreServices
    {
        public GenreServices(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public Genre AddGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            Genre newGenre = context.Genres.FirstOrDefault(g => g.GenreName == genreName);

            if (newGenre == null)
            {
                newGenre = this.context.Genres.Add(new Genre { GenreName = genreName }).Entity;
                this.context.SaveChanges();
            }

            return newGenre;
        }

        public Genre GetGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            return context.Genres.FirstOrDefault(g => g.GenreName == genreName);
        }
    }
}
