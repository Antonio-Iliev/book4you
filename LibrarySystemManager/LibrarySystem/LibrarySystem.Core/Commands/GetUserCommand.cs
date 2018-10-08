using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class GetUserCommand : ICommand
    {
        private IUsersServices usersServices;
        private readonly IAddressService addressService;
        private readonly ITownService townService;

        public GetUserCommand(IUsersServices usersServices, IAddressService addressService, ITownService townService)
        {
            this.usersServices = usersServices;
            this.addressService = addressService;
            this.townService = townService;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 3)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }
            var result = this.usersServices.GetUser(args[0], args[1], args[2]);

            if (result==null || result.IsDeleted == true)
            {
                return CommandConstants.UserDoesNotExist;
            }
            
            return $"User: {result.FirstName} {result.MiddleName} {result.LastName} {result.PhoneNumber} {result.Address.StreetAddress} {result.Address.Town.TownName}";
        }
    }
}
