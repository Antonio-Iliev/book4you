using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Constants;
using LibrarySystem.Services.Exceptions;
using LibrarySystem.Services.Exceptions.AddressServices;
using LibrarySystem.Services.Exceptions.TownServices;
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
            if (streetAddress == null) throw new AddAddressNullableExeption("Street Address can not be null!");
            if (town == null) throw new AddAddressNullableExeption("Town can not be null!");
            if (streetAddress.Length < 1) throw new InvalidAddressServiceParametersExeption($"Street Address is less then {ServicesConstants.MinAddressNameLength} symbol.");
            if (streetAddress.Length > 50) throw new InvalidAddressServiceParametersExeption($"Street Address is more then {ServicesConstants.MaxAddressNameLength} symbols.");
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
    }
}
