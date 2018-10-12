using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class AddressService : BaseServicesClass, IAddressService
    {
        public AddressService(UnitOfWork unitOfWork, IValidations validations)
            : base(unitOfWork, validations)
        {
        }

        public int AddAddress(string streetAddress, int town)
        {
            this.validations.AddressValidation(streetAddress, town);

            var address = this.unitOfWork.GetRepo<Address>().All()
                .FirstOrDefault(a => a.StreetAddress == streetAddress && a.TownId == town);

            if (address == null)
            {
                address = new Address()
                {
                    StreetAddress = streetAddress,
                    TownId = town
                };

                this.unitOfWork.GetRepo<Address>().Add(address);
                this.unitOfWork.SaveChanges();

                address = this.unitOfWork.GetRepo<Address>().All()
               .FirstOrDefault(a => a.StreetAddress == streetAddress && a.TownId == town);
            }

            return address.Id;
        }
    }
}
