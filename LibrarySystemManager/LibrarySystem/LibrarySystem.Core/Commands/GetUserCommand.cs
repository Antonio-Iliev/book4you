using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class GetUserCommand : ICommand
    {
        private readonly IUsersServices usersServices;
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
                throw new InvalidUserServiceParametersExeption("Invalid number of parameters.");
            }
            //var user = this.usersServices.GetUser(args[0], args[1], args[2]);

            //var result = new StringBuilder();

            //result.AppendLine($"Name: {user.FirstName} Phone: {user.PhoneNumber}");
            //result.AppendLine($"Added On: {user.AddOnDate} Address: {user.Address.StreetAddress}, {user.Address.Town.TownName}");
            //if (user.UsersBooks.Count > 0)
            //{
            //    result.AppendLine($"Read Books:{Environment.NewLine}{string.Join(Environment.NewLine, user.UsersBooks)}");
            //}
            //result.AppendLine("-------");

            return "";
        }
    }
}
