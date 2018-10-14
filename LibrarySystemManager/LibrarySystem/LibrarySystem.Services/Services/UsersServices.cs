using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(UnitOfWork unitOfWork, IValidations validations)
            : base(unitOfWork, validations)
        {
        }

        public UserViewModel AddUser(string firstName, string middleName, string lastName, string phoneNumber, DateTime addedOn, bool IsDeleted, int address)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.PhoneValidation(phoneNumber);

            var query = this.unitOfWork.GetRepo<User>().All()
               .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            UserViewModel userToReturn;

            if (query != null)
            {
                if (query.IsDeleted == true)
                {
                    query.IsDeleted = false;
                    if (query.PhoneNumber != phoneNumber)
                    {
                        UpdateUserPhone(firstName, middleName, lastName, phoneNumber);
                    }
                    if (query.AddressId != address)
                    {
                        UpdateUserAddress(firstName, middleName, lastName, address);
                    }
                }

                throw new UserNullableException("User already exists.");
            }
            else
            {

                var user = new User
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    AddOnDate = DateTime.Now,
                    IsDeleted = false,
                    AddressId = address
                };

                this.unitOfWork.GetRepo<User>().Add(user);
            }

            this.unitOfWork.SaveChanges();

            query = this.unitOfWork.GetRepo<User>().All()
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName
                );

            userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = phoneNumber,
                Address = query.Address.StreetAddress,
                Town = query.Address.Town.TownName,
                AddedOn = addedOn
            };

            return userToReturn;
        }

        public UserViewModel GetUser(string firstName, string middleName, string lastName)
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

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate,
                UserBooks = user.UsersBooks
            };

            return userToReturn;
        }

        public IEnumerable<UserViewModel> ListUsers(bool userIsDeleted)
        {
            var query = this.unitOfWork.GetRepo<User>().All()
                .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .Where(u => u.IsDeleted == userIsDeleted).ToList();


            var users = query.Select(u => new UserViewModel
            {
                FullName = $"{u.FirstName} {u.MiddleName} {u.LastName}",
                Phonenumber = u.PhoneNumber,
                Address = u.Address.StreetAddress,
                Town = u.Address.Town.TownName,
                AddedOn = u.AddOnDate,
                UserBooks = u.UsersBooks
            }).ToList();

            if (users.Count == 0)
            {
                throw new UserNullableException("No users were found.");
            }

            return users;
        }

        public UserViewModel RemoveUser(string firstName, string middleName, string lastName)
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

            user = this.unitOfWork.GetRepo<User>()
                .All().Include(u => u.Address)
                .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                    u => u.FirstName == firstName
                    && u.MiddleName == middleName
                    && u.LastName == lastName
                );

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate
            };

            return userToReturn;
        }
        public UserViewModel UpdateUserAddress(string firstName, string middleName, string lastName, int address)
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
            user.AddressId = address;
            this.unitOfWork.GetRepo<User>().Update(user);

            this.unitOfWork.SaveChanges();

            user = this.unitOfWork.GetRepo<User>()
               .All().Include(u => u.Address)
               .ThenInclude(a => a.Town)
               .Include(u => u.UsersBooks)
               .ThenInclude(ub => ub.Book)
               .SingleOrDefault(
                   u => u.FirstName == firstName
                   && u.MiddleName == middleName
                   && u.LastName == lastName
               );

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate
            };

            return userToReturn;
        }

        public UserViewModel UpdateUserPhone(string firstName, string middleName, string lastName, string phone)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.PhoneValidation(phone);

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

            user = this.unitOfWork.GetRepo<User>()
             .All().Include(u => u.Address)
             .ThenInclude(a => a.Town)
             .Include(u => u.UsersBooks)
             .ThenInclude(ub => ub.Book)
             .SingleOrDefault(
                 u => u.FirstName == firstName
                 && u.MiddleName == middleName
                 && u.LastName == lastName
             );

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate
            };

            return userToReturn;
        }
        public UserViewModel BorrowBook(string firstName, string middleName, string lastName, string bookTitle)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.BookTitleValidation(bookTitle);

            var user = this.unitOfWork.GetRepo<User>().All()
                  .SingleOrDefault(u => u.FirstName == firstName
                  && u.MiddleName == middleName
                  && u.LastName == lastName);

            if (user == null)
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
            this.unitOfWork.SaveChanges();

            user = this.unitOfWork.GetRepo<User>()
            .All().Include(u => u.Address)
            .ThenInclude(a => a.Town)
            .Include(u => u.UsersBooks)
            .ThenInclude(ub => ub.Book)
            .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName
            );

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate
            };

            return userToReturn;
        }

        public UserViewModel ReturnBook(string firstName, string middleName, string lastName, string bookTitle)
        {
            this.validations.UserValidation(firstName, middleName, lastName);
            this.validations.BookTitleValidation(bookTitle);

            var user = this.unitOfWork.GetRepo<User>().All()
               .SingleOrDefault(u => u.FirstName == firstName
               && u.MiddleName == middleName
               && u.LastName == lastName);

            if (user == null)
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

            user = this.unitOfWork.GetRepo<User>()
            .All().Include(u => u.Address)
            .ThenInclude(a => a.Town)
            .Include(u => u.UsersBooks)
            .ThenInclude(ub => ub.Book)
            .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName
            );

            UserViewModel userToReturn = new UserViewModel
            {
                FullName = $"{firstName} {middleName} {lastName}",
                Phonenumber = user.PhoneNumber,
                Address = user.Address.StreetAddress,
                Town = user.Address.Town.TownName,
                AddedOn = user.AddOnDate
            };

            return userToReturn;
        }
    }
}
