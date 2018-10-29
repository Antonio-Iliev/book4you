using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class AddressService : BaseServicesClass, IAddressService
    {
        public AddressService(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public Address AddAddress(string streetAddress, Town town)
        {
            this.validations.AddressValidation(streetAddress, town);

            var address = this.context.Addresses
                .FirstOrDefault(a => a.StreetAddress == streetAddress && a.TownId == town.Id);

            if (address == null)
            {
                address = new Address()
                {
                    StreetAddress = streetAddress,
                    TownId = town.Id
                };

                this.context.Addresses.Add(address);
                this.context.SaveChanges();
            }

            address.Town = town;

            return address;
        }
    }
}
