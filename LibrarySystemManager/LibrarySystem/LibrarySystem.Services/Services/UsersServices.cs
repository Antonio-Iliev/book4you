using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.UserServices;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(UnitOfWork unitOfWork, IValidations validations)
            : base(unitOfWork, validations)
        {
        }

        public User AddUser(string firstName, string middleName, string lastName, string phoneNumber, DateTime addedOn, bool IsDeleted, Address address)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var query = this.unitOfWork.GetRepo<User>().All()
               .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (query != null)
            {
                throw new UserNullableException("User already exists.");
            }

            var user = new User
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = address.Id
            };

            this.unitOfWork.GetRepo<User>().Add(user);
            this.unitOfWork.SaveChanges();

            return user;
        }

        public User GetUser(string firstName, string middleName, string lastName)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.unitOfWork.GetRepo<User>().All()
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName
                );

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableException("This user does not exists.");
            }

            return user;
        }

        public IEnumerable<User> ListUsers()
        {
            var users = this.unitOfWork.GetRepo<User>().All()
                .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .Where(u => !u.IsDeleted)
                .ToList();

            if (users.Count == 0)
            {
                throw new UserNullableException("No users were found.");
            }
            return users;
        }

        public User RemoveUser(string firstName, string middleName, string lastName)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.unitOfWork.GetRepo<User>().All()
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null)
            {
                throw new UserNullableException("This user does not exist.");
            }

            user.IsDeleted = true;
            this.unitOfWork.SaveChanges();

            return user;
        }
        public User UpdateUserAddress(string firstName, string middleName, string lastName, Address address)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.unitOfWork.GetRepo<User>().All()
                .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableException("This user does not exist.");
            }
            user.AddressId = address.Id;
            this.unitOfWork.GetRepo<User>().Update(user);

            this.unitOfWork.SaveChanges();
            return user;
        }

        public User UpdateUserPhone(string firstName, string middleName, string lastName, string phone)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.unitOfWork.GetRepo<User>().All()
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableException("This user does not exist.");
            }

            user.PhoneNumber = phone.ToString();
            this.unitOfWork.SaveChanges();

            return user;
        }
        public User BorrowBook(string firstName, string middleName, string lastName, string bookTitle)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.BookTitleValidation(bookTitle);

            var currentUser = this.unitOfWork.GetRepo<User>().All()
                  .SingleOrDefault(u => u.FirstName == firstName
                  && u.MiddleName == middleName
                  && u.LastName == lastName);

            if (currentUser == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            var bookForBorrow = this.unitOfWork.GetRepo<Book>().All().FirstOrDefault(b => b.Title == bookTitle);

            if (bookForBorrow == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }
            if (bookForBorrow.BooksInStore - 1 < 0)
            {
                throw new AddBookNullableExeption("There is no enough books in store");
            }

            var isBorrow = this.unitOfWork.GetRepo<UsersBooks>().All()
                .Select(b => b)
                .Where(b => b.BookId == bookForBorrow.Id && b.UserId == currentUser.Id).ToList();

            if (isBorrow.Count != 0)
            {
                throw new AddBookNullableExeption($"User {firstName} already borrow this book '{bookTitle}'.");
            }

            bookForBorrow.BooksInStore--;

            var usersBooks = new UsersBooks
            {
                User = currentUser,
                Book = bookForBorrow
            };

            currentUser.UsersBooks.Add(usersBooks);
            this.unitOfWork.SaveChanges();

            return currentUser;
        }

        public User ReturnBook(string firstName, string middleName, string lastName, string bookTitle)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.BookTitleValidation(bookTitle);

            var currentUser = this.unitOfWork.GetRepo<User>().All()
               .SingleOrDefault(u => u.FirstName == firstName
               && u.MiddleName == middleName
               && u.LastName == lastName);

            if (currentUser == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            var bookToReturn = this.unitOfWork.GetRepo<Book>().All().FirstOrDefault(b => b.Title == bookTitle);

            if (bookToReturn == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }

            bookToReturn.BooksInStore++;
            this.unitOfWork.SaveChanges();

            return currentUser;
        }
    }
}
