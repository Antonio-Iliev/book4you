using LibrarySystem.Services.Abstract;
using System.Linq;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Data.Context;

namespace LibrarySystem.Services.Services
{
    public class TownService : BaseServicesClass, ITownService
    {
        public TownService(ILibrarySystemContext context, IValidations validations) 
            : base(context, validations)
        {
        }

        public int AddTown(string townName)
        {
            this.validations.TownValidation(townName);

            var town = this.context.Towns.FirstOrDefault(t => t.TownName == townName);

            if (town == null)
            {
                this.context.Towns.Add(new Town() { TownName = townName });
                this.context.SaveChanges();
                town = this.context.Towns.FirstOrDefault(t => t.TownName == townName);
            }

            return town.Id;
        }
    }
}
