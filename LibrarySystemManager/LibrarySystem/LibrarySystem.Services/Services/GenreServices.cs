using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class GenreServices : BaseServicesClass, IGenreServices
    {
        public GenreServices(UnitOfWork unitOfWork, IValidations validations)
            : base(unitOfWork, validations)
        {
        }

        public int AddGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            Genre newGenre = this.unitOfWork.GetRepo<Genre>().All().FirstOrDefault(g => g.GenreName == genreName);

            if (newGenre == null)
            {
                this.unitOfWork.GetRepo<Genre>().Add(new Genre { GenreName = genreName });
                this.unitOfWork.SaveChanges();
                newGenre = this.unitOfWork.GetRepo<Genre>().All().FirstOrDefault(g => g.GenreName == genreName);
            }

            return newGenre.Id;
        }

        public Genre GetGenre(string genreName)
        {
            this.validations.GenreValidation(genreName);

            return this.unitOfWork.GetRepo<Genre>().All().FirstOrDefault(g => g.GenreName == genreName);
        }
    }
}
