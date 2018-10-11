using LibrarySystem.Services.Abstract;
using System.Linq;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;

namespace LibrarySystem.Services.Services
{
    public class TownService : BaseServicesClass, ITownService
    {
        public TownService(UnitOfWork unitOfWork, IValidations validations) 
            : base(unitOfWork, validations)
        {
        }

        public Town AddTown(string townName)
        {
            this.validations.TownValidation(townName);

            var town = this.unitOfWork.GetRepo<Town>().All().FirstOrDefault(t => t.TownName == townName);

            if (town == null)
            {
                this.unitOfWork.GetRepo<Town>().Add(new Town() { TownName = townName });
                this.unitOfWork.SaveChanges();
                town = this.unitOfWork.GetRepo<Town>().All().FirstOrDefault(t => t.TownName == townName);
            }

            return town;
        }

    }
}
