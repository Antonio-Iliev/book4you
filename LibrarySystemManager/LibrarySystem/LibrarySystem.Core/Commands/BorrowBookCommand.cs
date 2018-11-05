using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Services.Exceptions.UserServices;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class BorrowBookCommand : ICommand
    {
        private readonly IUsersServices usersServices;

        public BorrowBookCommand(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            IList<string> args = parameters.ToList();

            if (args.Count != 4)
            {
                throw new InvalidUserServiceParametersExeption("Invalid numbers of parameters");
            }

            string firstName = args[0];
            string middName = args[1];
            string lastName = args[2];
            string book = args[3];

            // this method don't work var userWithBook = usersServices.BorrowBook();

            return $"User {firstName} " +
                $"borrow the book {book}";
        }
    }
}
