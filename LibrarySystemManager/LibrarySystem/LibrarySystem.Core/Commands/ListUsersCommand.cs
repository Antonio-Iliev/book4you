using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class ListUsersCommand : BaseCommand
    {
        private IUsersServices usersServices;
        public ListUsersCommand(IUsersServices usersServices, ILibrarySystemContext libraryContext) : base(libraryContext)
        {
            this.usersServices = usersServices;
        }

        public override string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();
            int count = 0;
           
            if (args.Count == 0)
            {
                var users = this.usersServices.ListUsers(null, null, null);
                count = users.Count();
            }
            else if (args.Count != 3)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }
            else
            {
                var users = this.usersServices.ListUsers(args[0], args[1], args[2]);
                count = 1;
            }
            return $"{count} user/s found";
        }
    }
}
