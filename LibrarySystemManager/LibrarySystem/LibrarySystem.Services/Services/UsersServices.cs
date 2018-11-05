using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Context;
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
        public UsersServices(LibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public User AddUser
            (string firstName, string middleName, string lastName,
            string phoneNumber,
            DateTime addedOn,
            bool IsDeleted,
            Address address)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.PhoneValidation(phoneNumber);

            var user = this.context.Users
               .Include(u => u.Address).ThenInclude(a => a.Town)
               .Include(u => u.UsersBooks).ThenInclude(ub => ub.Book)
               .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user != null)
            {
                if (user.IsDeleted == true)
                {
                    user.IsDeleted = false;
                    if (user.PhoneNumber != phoneNumber)
                    {
                        UpdateUserPhone(firstName, middleName, lastName, phoneNumber);
                    }
                    if (user.AddressId != address.Id)
                    {
                        UpdateUserAddress(firstName, middleName, lastName, address);
                    }

                    this.context.SaveChanges();
                }
                else
                {
                    throw new UserNullableException("User already exists.");
                }
            }
            else
            {
                var newUser = new User
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    AddOnDate = DateTime.Now,
                    IsDeleted = false,
                    AddressId = address.Id,
                    Address = address
                };

                this.context.Users.Add(newUser);
                this.context.SaveChanges();
                user = newUser;
            }

            user.Address = address;

            return user;
        }

        public User GetUser(string firstName, string middleName, string lastName)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.context.Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                u => u.FirstName == firstName
              //  && u.MiddleName == middleName
                && u.LastName == lastName
                );

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableException("This user does not exists.");
            }

            return user;
        }

        public IEnumerable<User> ListUsers(bool userIsDeleted)
        {
            var user = this.context.Users
                  .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                  .Include(u => u.UsersBooks)
                  .ThenInclude(ub => ub.Book)
                  .Where(u => u.IsDeleted == userIsDeleted).ToList();

            if (user.Count == 0)
            {
                throw new UserNullableException("No users were found.");
            }

            return user;
        }

        public User RemoveUser(string firstName, string middleName, string lastName)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.context.Users
                .Include(u => u.Address)
                .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                .ThenInclude(ub => ub.Book)
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null)
            {
                throw new UserNullableException("This user does not exist.");
            }

            user.IsDeleted = true;
            this.context.SaveChanges();

            return user;
        }

        public User UpdateUserAddress(string firstName, string middleName, string lastName, Address address)
        {
            this.validations.UserValidation(firstName, middleName, lastName);

            var user = this.context.Users
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
            user.Address = address;
            this.context.Users.Update(user);

            this.context.SaveChanges();

            return user;
        }

        public User UpdateUserPhone(string firstName, string middleName, string lastName, string phone)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.PhoneValidation(phone);

            var user = this.context.Users
                .Include(u => u.Address)
                .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableException("This user does not exist.");
            }

            user.PhoneNumber = phone.ToString();
            this.context.SaveChanges();

            return user;
        }

        public User BorrowBook(string firstName, string middleName, string lastName, string bookTitle)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.BookTitleValidation(bookTitle);

            var user = this.context.Users
                  .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                  .Include(u => u.UsersBooks)
                  .ThenInclude(ub => ub.Book)
                  .FirstOrDefault(u => u.FirstName == firstName
                  && u.MiddleName == middleName
                  && u.LastName == lastName);

            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            var bookForBorrow = this.context.Books
                .Include(a => a.Author)
                .Include(g => g.Genre)
                .FirstOrDefault(b => b.Title == bookTitle);

            if (bookForBorrow == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }
            if (bookForBorrow.BooksInStore - 1 < 0)
            {
                throw new AddBookNullableExeption("There is no enough books in store");
            }

            var isBorrow = this.context.UsersBooks
                .Select(b => b)
                .Where(b => b.BookId == bookForBorrow.Id && b.UserId == user.Id).ToList();

            if (isBorrow.Count != 0)
            {
                throw new AddBookNullableExeption($"User {firstName} already borrow this book '{bookTitle}'.");
            }

            bookForBorrow.BooksInStore--;

            var usersBooks = new UsersBooks
            {
                User = user,
                Book = bookForBorrow
            };

            user.UsersBooks.Add(usersBooks);
            this.context.SaveChanges();

            return user;
        }

        public Book ReturnBook(string userId, Guid bookId)
        {
            var bookToReturn = this.context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .SingleOrDefault(b => b.Id == bookId);

            if (bookToReturn == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }

            var userBook = this.context.UsersBooks
                .SingleOrDefault(ub => ub.UserId == userId && ub.BookId == bookId);

            if (userBook == null)
            {
                throw new UserNullableException("This user never borrowed this book.");
            }

            userBook.IsReturn = true;
            bookToReturn.BooksInStore++;

            this.context.SaveChanges();

            return bookToReturn;
        }

        public User GetUserById(string id)
        {
            var user=this.context.Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
               throw new UserNullableException("There is no such user in this Library.");
            }

            return user;
        }
        public User RemoveUserById(string id)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            user.IsDeleted = true;
            this.context.SaveChanges();

            return user;
        }
    }
}
