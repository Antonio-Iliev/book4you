using LibrarySystem.Data.Contracts;
using LibrarySystem.Services.Abstract;
using System.Linq;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;

namespace LibrarySystem.Services.Services
{
    public class TownService : BaseServicesClass, ITownService
    {
        public TownService(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public Town AddTown(string townName)
        {
            this.validations.TownValidation(townName);

            var town = this.context.Towns.FirstOrDefault(t => t.TownName == townName);

            if (town == null)
            {
                town = this.context.Towns.Add(new Town() { TownName = townName }).Entity;
                this.context.SaveChanges();
            }

            return town;
        }

    }
}
