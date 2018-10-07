using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class AddUserCommand : BaseCommand
    {
        public AddUserCommand(ILibrarySystemContext libraryContext) : base(libraryContext)
        {
        }
        //addUser firstName, middleName, lastName, int phoneNumber, DateTime addedOn, bool IsDeleted
        // Address & books TO DO
        public override string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();
            if (this.LibraryContext.Users
                .Any(u => u.FirstName == args[0]
                && u.MiddleName == args[1]
                && u.LastName == args[2]))
            {
                throw new ArgumentException(CommandConstants.UserAlreadyExists);
            }
            var user = new User
            {
                FirstName = args[0],
                MiddleName = args[1],
                LastName = args[2],
                PhoneNumber = int.Parse(args[3]),
                AddOnDate = DateTime.Now,
                IsDeleted = false,
            };
            this.LibraryContext.Users.Add(user);
            this.LibraryContext.SaveChanges();

            return $"Created user with id {user.Id}";
        }
    }
}
