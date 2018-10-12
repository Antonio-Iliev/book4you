using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class UpdateUserAddressCommand : ICommand
    {
        private readonly IUsersServices usersServices;
        private readonly IAddressService addressService;
        private readonly ITownService townService;

        public UpdateUserAddressCommand(IUsersServices usersServices, IAddressService addressService, ITownService townService)
        {
            this.usersServices = usersServices;
            this.addressService = addressService;
            this.townService = townService;
        }
        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 5)
            {
                throw new InvalidUserServiceParametersExeption("Invalid number of parameters.");
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];

            Town town = townService.AddTown(args[4]);
            Address newAddress = addressService.AddAddress(args[3], town);

            this.usersServices.UpdateUserAddress(firstName, middleName, lastName, newAddress);

            return $"The address of user: {firstName} {middleName} {lastName} was successfully updated to {args[3]}, {args[4]}";
        }
    }
}
