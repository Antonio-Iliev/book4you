using LibrarySystem.Data.Contracts;
using LibrarySystem.Services.Abstract;
using System.Linq;
using System.Collections.Generic;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.TownServices;
using LibrarySystem.Services.Constants;

namespace LibrarySystem.Services.Services
{
    public class TownService : BaseServicesClass, ITownService
    {
        public TownService(ILibrarySystemContext context) : base(context)
        {
        }

        public Town AddTown(string townName)
        {
            if (townName == null) throw new AddTownNullableExeption("Town name can not be null!");
            if (townName.Length < 1) throw new InvalidTownServiceParametersExeption($"The town name is less then {ServicesConstants.MinTownNameLength} symbol.");
            if (townName.Length > 50) throw new InvalidTownServiceParametersExeption($"The town name is more then {ServicesConstants.MaxTownNameLength} symbols.");

            var town = base.context.Towns.FirstOrDefault(t => t.TownName == townName);

            if (town == null)
            {
                town = base.context.Towns.Add(new Town() { TownName = townName }).Entity;
                base.context.SaveChanges();
            }

            return town;
        }

    }
}
