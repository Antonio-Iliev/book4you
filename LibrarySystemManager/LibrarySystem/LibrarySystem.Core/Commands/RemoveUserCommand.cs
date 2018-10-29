using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class RemoveUserCommand : ICommand
    {
        private readonly IUsersServices usersServices;

        public RemoveUserCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 3)
            {
                throw new InvalidUserServiceParametersExeption("Invalid number of parameters.");
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];

            var user = this.usersServices.RemoveUser(firstName, middleName, lastName);

            return $"User {user.FirstName} was successfully deleted.";
        }
    }
}
