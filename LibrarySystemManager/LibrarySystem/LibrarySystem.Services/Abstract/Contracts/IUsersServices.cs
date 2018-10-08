using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IUsersServices
    {
        User AddUser(string firstName, string middleName, string lastName,
            int phoneNumber, DateTime addedOn, bool IsDeleted, Address address);

        User UpdateUser(string firstName, string middleName, string lastName,
            int phoneNumber, DateTime addedOn, bool IsDeleted, Address address, ICollection<UsersBooks> books);

        User RemoveUser(string firstName, string middleName, string lastName);

        User GetUser(string firstName, string middleName, string lastName);
        
        IEnumerable<User> ListUsers();

    }
}