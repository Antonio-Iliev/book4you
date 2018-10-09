using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class UpdateUserCommand : ICommand
    {
        private readonly IUsersServices usersServices;
        private readonly IAddressService addressService;
        private readonly ITownService townService;

        public UpdateUserCommand(IUsersServices usersServices, IAddressService addressService, ITownService townService)
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
                throw new ArgumentException("InvalidNumbersOfParameters");
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];
            Town town = townService.AddTown(args[4]);
            Address newAddress = addressService.AddAddress(args[3], town);

            var user = this.usersServices.UpdateUser(firstName, middleName, lastName, newAddress);

            return $"The address of user: {user.FirstName} {user.MiddleName} {user.LastName} was successfully updated to {user.Address.StreetAddress}, {user.Address.Town.TownName}";
        }
    }
}
