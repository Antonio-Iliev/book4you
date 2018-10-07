using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class RemoveUserCommand : BaseCommand
    {
        private IUsersServices usersServices;
        public RemoveUserCommand(IUsersServices usersServices,ILibrarySystemContext libraryContext) : base(libraryContext)
        {
            this.usersServices = usersServices;
        }

        public override string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 3)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];

            var user = this.usersServices.RemoveUser(firstName, middleName, lastName);

            return $"User {firstName} {middleName} {lastName} successfully deleted";
        }
    }
}
