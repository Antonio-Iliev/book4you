using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class GetUserCommand : ICommand
    {
        private IUsersServices usersServices;

        public GetUserCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
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

            return $"User: {result.Id} {result.FirstName} {result.MiddleName} {result.LastName} {result.PhoneNumber} {result.UserAddresses}";
        }
    }
}
