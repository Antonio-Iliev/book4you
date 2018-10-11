using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System.Collections.Generic;
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
                var books = new StringBuilder();

                foreach (var item in user.UsersBooks)
                {
                    books.AppendLine(item.Book.Title);
                }
                result.AppendLine(
                    $"Name: {user.FirstName} {user.MiddleName} {user.LastName} Phone: {user.PhoneNumber} " +
                    $"Added On: {user.AddOnDate} Address: {user.Address.StreetAddress}, {user.Address.Town.TownName} " +
                    $"{books.ToString()}."
                    );
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}
