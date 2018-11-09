using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IUsersServices
    {
        IEnumerable<User> ListUsers(string listUsersBy, int pageSize, int page);

        User BorrowBook(string userId, Guid bookId);

        Book ReturnBook(string userId, Guid bookId);

        User GetUserById(string id);

        User RemoveUserById(string id);

        User BorrowBook(string id, string bookTitle);

        User ReturnBook(string id, string bookTitle);

        User UpdateUser(string id, string firstName, string middleName, string lastName,
            string phone, Address address);

        User RestoreUserById(string id);
    }
}