using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Services.Services
{
    public class AddressService : BaseServicesClass
    {
        public AddressService(ILibrarySystemContext context) : base(context)
        {
        }

        public Address AddAddress(string streetAddress, Town town)
        {
            var address = new Address()
            {
                StreetAddress = streetAddress,
                TownId = town.Id
            };

            var dbAddress = context.Addresses
                .FirstOrDefault((adr) => adr.StreetAddress == streetAddress && adr.Id == town.Id);

            if (dbAddress == null)
            {
                base.context.Addresses.Add(address);
                context.SaveChanges();
            }

            return address;
        }

        public IEnumerable<Address> GetAddress(string streetAddress)
        {
            return context.Addresses.Where(a => a.StreetAddress == streetAddress);
        }

    }
}
