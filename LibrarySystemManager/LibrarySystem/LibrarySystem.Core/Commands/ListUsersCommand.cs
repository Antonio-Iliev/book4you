using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
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
            var users = this.usersServices.ListUsers();

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
