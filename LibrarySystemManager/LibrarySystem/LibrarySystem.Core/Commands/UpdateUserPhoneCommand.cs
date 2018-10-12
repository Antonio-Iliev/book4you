using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class UpdateUserPhoneCommand : ICommand
    {
        private readonly IUsersServices usersServices;

        public UpdateUserPhoneCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }
        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 4)
            {
                throw new InvalidUserServiceParametersExeption("Invalid number of parameters.");
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];
            var newPhone = args[3];

            var user = this.usersServices.UpdateUserPhone(firstName, middleName, lastName, newPhone);

            return $"The phone of user: {user.FullName} was successfully updated to {user.FullName}";
        }
    }
}
