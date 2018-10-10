using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                throw new ArgumentException("Invalid numbers of parameters");
            }

            string firstName = args[0];
            string middName = args[1];
            string lastName = args[2];
            string book = args[3];

            var userWithBook = usersServices.BorrowBook(firstName, middName, lastName, book);

            return $"User {userWithBook.FirstName} {userWithBook.MiddleName} {userWithBook.LastName} " +
                $"borrow the book {book}";
        }
    }
}
