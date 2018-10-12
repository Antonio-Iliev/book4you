using LibrarySystem.Data.Models;
using LibrarySystem.Services.ViewModels;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IUsersServices
    {
        UserViewModel AddUser(string firstName, string middleName, string lastName,
            string phoneNumber, DateTime addedOn, bool IsDeleted, int address);

        UserViewModel UpdateUserAddress(string firstName, string middleName, string lastName, int address);

        UserViewModel UpdateUserPhone(string firstName, string middleName, string lastName, string phone);

        UserViewModel RemoveUser(string firstName, string middleName, string lastName);

        UserViewModel GetUser(string firstName, string middleName, string lastName);
        
        IEnumerable<UserViewModel> ListUsers();

        UserViewModel BorrowBook(string firstName, string middleName, string lastName, string bookTitle);

        UserViewModel ReturnBook(string firstName, string middleName, string lastName, string bookTitle);

    }
}