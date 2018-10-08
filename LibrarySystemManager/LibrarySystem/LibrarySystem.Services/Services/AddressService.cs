using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Services.Services
{
    public class AddressService : BaseServicesClass, IAddressService
    {
        public AddressService(ILibrarySystemContext context) : base(context)
        {
        }
        
        public Address AddAddress(string streetAddress, Town town)
        {
            var address = base.context.Addresses
                .FirstOrDefault(a => a.StreetAddress == streetAddress && a.TownId == town.Id);

            if (address == null)
            {
                address = new Address()
                {
                    StreetAddress = streetAddress,
                    TownId = town.Id
                };

                address = base.context.Addresses.Add(address).Entity;
                base.context.SaveChanges();
            }

            return address;
        }

        public IEnumerable<Address> GetAddress(string streetAddress)
        {
            return context.Addresses.Where(a => a.StreetAddress == streetAddress);
        }

    }
}
