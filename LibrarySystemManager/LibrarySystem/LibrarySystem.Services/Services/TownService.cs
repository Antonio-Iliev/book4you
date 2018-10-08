using LibrarySystem.Data.Contracts;
using LibrarySystem.Services.Abstract;
using System.Linq;
using System.Collections.Generic;
using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public class TownService : BaseServicesClass, ITownService
    {
        public TownService(ILibrarySystemContext context) : base(context)
        {
        }
        public Town AddTown(string townName)
        {
            var town = new Town()
            {
                TownName = townName
            };

            var dbTown = context.Towns.SingleOrDefault(t => t.TownName == townName);

            if (dbTown == null)
            {
                base.context.Towns.Add(town);
                context.SaveChanges();
            }

            return town;
        }

    }
}
