using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Services.Services
{
    public class GenreServices : BaseServicesClass, IGenreServices
    {
        public GenreServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Genre AddGenre(string genreName)
        {
            Genre newGenre = context.Genres.FirstOrDefault(g => g.GenreName == genreName);

            if (newGenre == null)
            {
                this.context.Genres.Add(new Genre { GenreName = genreName });
                this.context.SaveChanges();
                newGenre = context.Genres.FirstOrDefault(g => g.GenreName == genreName);
            }

            return newGenre;
        }

        public Genre GetGenre(string genreName)
        {
            return context.Genres.FirstOrDefault(g => g.GenreName == genreName);
        }
    }
}
