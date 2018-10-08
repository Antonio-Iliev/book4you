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
