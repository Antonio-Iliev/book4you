using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class ListUsersCommand : ICommand
    {
        private IUsersServices usersServices;

        public ListUsersCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            var users = this.usersServices.ListUsers();

            var result = new StringBuilder();

            foreach (var user in users)
            {
                result.Append(user);
                result.Append('\n');
            }

            return result.ToString();
        }
    }
}
