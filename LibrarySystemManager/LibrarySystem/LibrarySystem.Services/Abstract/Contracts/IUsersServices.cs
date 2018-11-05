using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IUsersServices
    {
        User AddUser(string firstName, string middleName, string lastName,
            string phoneNumber, DateTime addedOn, bool IsDeleted, Address address);

        User UpdateUserAddress(string firstName, string middleName, string lastName, Address address);

        User UpdateUserPhone(string firstName, string middleName, string lastName, string phone);

        User RemoveUser(string firstName, string middleName, string lastName);

        User GetUser(string firstName, string middleName, string lastName);
        
        IEnumerable<User> ListUsers(bool userIsDeleted);

        User BorrowBook(string firstName, string middleName, string lastName, string bookTitle);

        Book ReturnBook(string userId, Guid bookId);

        User GetUserById(string id);

        User RemoveUserById(string id);
    }
}