using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IUsersServices usersServices;

        public ReturnBookCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            IList<string> args = parameters.ToList();

            if (args.Count != 4)
            {
                throw new ArgumentException("Invalid numbers of parameters");
            }

            string firstName = args[0];
            string middName = args[1];
            string lastName = args[2];
            string book = args[3];

            var user = usersServices.ReturnBook(firstName, middName, lastName, book);

            return $"User {user.FullName} return the book {book}";
        }
    }
}
