using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
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
                throw new InvalidBookServiceParametersExeption("Invalid numbers of parameters");
            }

            string id = args[0];
           
            string book = args[1];

            var user = usersServices.ReturnBook(id, book);

            return $"User {user.FirstName} return the book {book}";
        }
    }
}
