using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class ListUsersCommand : ICommand
    {
        private readonly IUsersServices usersServices;

        public ListUsersCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }
        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 1)
            {
                throw new InvalidUserServiceParametersExeption("Invalid number of parameters.");
            }

            bool userIsDeleted;

            switch (args[0].ToLower())
            {
                case "active":
                    userIsDeleted = false;
                    break;

                case "deleted":
                    userIsDeleted = true;
                    break;

                default:
                    throw new InvalidUserServiceParametersExeption("Invalid parameter. It should be 'active' or 'deleted'");
            }

            var users = this.usersServices.ListUsers(userIsDeleted);

            var result = new StringBuilder();            

            foreach (var user in users)
            {
                var books = user.UserBooks.Select(x => x.Book.Title).ToList();
                
                result.AppendLine(
                    $"Name: {user.FullName} Phone: {user.Phonenumber} " +
                    $"Added On: {user.AddedOn} Address: {user.Address}, {user.Town} " +
                    $"{string.Join(", ", books)}."
                    );
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}
